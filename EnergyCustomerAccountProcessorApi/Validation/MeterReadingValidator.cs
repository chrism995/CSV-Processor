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

            // Ensure the entry is unique
            var duplicateExists = await _context.MeterReadings.AnyAsync(m =>
                m.AccountId == meterReading.AccountId &&
                m.MeterReadingDateTime == meterReading.MeterReadingDateTime &&
                m.MeterReadValue == meterReading.MeterReadValue);

            if (duplicateExists)
            {
                return false;
            }

            return true;
        }
    }
}
