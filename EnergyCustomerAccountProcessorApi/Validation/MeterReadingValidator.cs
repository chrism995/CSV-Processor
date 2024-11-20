using EnergyCustomerAccountProcessorApi.Data;
using EnergyCustomerAccountProcessorApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace EnergyCustomerAccountProcessorApi.Validation
{
    public class MeterReadingValidator : IMeterReadingValidator
    {
        private readonly EnergyContext _context;

        public MeterReadingValidator(EnergyContext context)
        {
            _context = context;
        }

        public async Task<bool> ValidateMeterReadingAsync(MeterReading meterReading)
        {
            if (meterReading == null)
            {
                return false;
            }

            // Validate AccountId
            var accountExists = await IsAccountValidAsync(meterReading.AccountId);
            if (!accountExists)
            {
                return false;
            }

            // Validate MeterReadValue format
            if (!IsMeterReadingValueValid(meterReading.MeterReadValue.ToString()))
            {
                return false;
            }

            // Save or update meter reading
            var isSaved = await SaveOrUpdateMeterReadingAsync(meterReading);
            return isSaved;
        }

        // Method to validate if account exists
        private async Task<bool> IsAccountValidAsync(int accountId)
        {
            return await _context.Users.AnyAsync(u => u.AccountID == accountId);
        }

        // Method to validate MeterReadValue format
        private bool IsMeterReadingValueValid(string meterReadValue)
        {
            return Regex.IsMatch(meterReadValue, @"^\d{5}$");
        }

        // Method to save or update meter reading in the database
        private async Task<bool> SaveOrUpdateMeterReadingAsync(MeterReading meterReading)
        {
            var existingMeterReading = await _context.MeterReadings
                .FirstOrDefaultAsync(m => m.AccountId == meterReading.AccountId);

            if (existingMeterReading != null)
            {
                existingMeterReading.MeterReadValue = meterReading.MeterReadValue;
                _context.MeterReadings.Update(existingMeterReading);
            }
            else
            {
                await _context.MeterReadings.AddAsync(meterReading);
            }

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
