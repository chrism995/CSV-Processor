using EnergyCustomerAccountProcessorApi.Data;
using EnergyCustomerAccountProcessorApi.Services;
using EnergyCustomerAccountProcessorApi.Validation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EnergyContext>(options =>
    options.UseSqlite("Data Source=energy.db")); 

builder.Services.AddScoped<IMeterReadingService, MeterReadingService>();
builder.Services.AddScoped<IMeterReadingValidator, MeterReadingValidator>();
builder.Services.AddScoped<UserSeeder>();

var app = builder.Build();

// Ensure the database is created and migrate any pending changes.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<EnergyContext>();
    var seeder = services.GetRequiredService<UserSeeder>();

    await context.Database.MigrateAsync();  // Apply any pending migrations
    await seeder.SeedUsersAsync("accounts.csv");  // Seed the users
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

