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

            // Check for valid AccountId
            var accountExists = await _context.Users.AnyAsync(u => u.AccountID == meterReading.AccountId);
            if (!accountExists)
            {
                return false;
            }

            // Validate MeterReadValue format (NNNNN)
            if (!Regex.IsMatch(meterReading.MeterReadValue.ToString(), @"^\d{5}$"))
            {
                return false;
            }

            // Check if the meter reading already exists
            var existingMeterReading = await _context.MeterReadings
                .FirstOrDefaultAsync(m => m.AccountId == meterReading.AccountId);

            if (existingMeterReading != null)
            {
                // If found, update the existing record (e.g., update MeterReadValue)
                existingMeterReading.MeterReadValue = meterReading.MeterReadValue;

                // You can perform any other updates to the existing record here, if necessary
                _context.MeterReadings.Update(existingMeterReading);
            }
            else
            {
                // If not found, add a new record
                await _context.MeterReadings.AddAsync(meterReading);
            }

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch
            {
                // Log or handle the exception if necessary
                return false;
            }

            return true;
        }
    }
}
