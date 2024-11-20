using EnergyCustomerAccountProcessorApi.Data;
using EnergyCustomerAccountProcessorApi.Services;
using EnergyCustomerAccountProcessorApi.Validation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EnergyContext>(options =>
    options.UseSqlite("Data Source=energy.db")); 

builder.Services.AddScoped<IMeterReadingService, MeterReadingService>();
builder.Services.AddScoped<IMeterReadingValidator, MeterReadingValidator>();
builder.Services.AddScoped<UserSeeder>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

