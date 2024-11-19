// Services/UserSeeder.cs
using CsvHelper;
using CsvHelper.Configuration;
using EnergyCustomerAccountProcessorApi.Models;
using EnergyCustomerAccountProcessorApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EnergyCustomerAccountProcessorApi.Services
{
    public class UserSeeder
    {
        private readonly EnergyContext _context;

        public UserSeeder(EnergyContext context)
        {
            _context = context;
        }

        public async Task SeedUsersAsync(string csvFilePath)
        {
            // Read the CSV file
            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

            // Parse the CSV into a list of User objects
            var users = csv.GetRecords<User>().ToList();

            // Add to the database, checking for duplicates
            foreach (var user in users)
            {
                // Only add if not already in the database (avoid duplicates)
                if (!_context.Users.Any(u => u.AccountID == user.AccountID))
                {
                    _context.Users.Add(user);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
