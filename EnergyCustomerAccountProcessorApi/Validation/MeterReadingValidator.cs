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

        /// <test cases - for happy path using NUnit>
        /// Validate a valid meter reading with existing account and correct format
        /// Validate a valid meter reading with non-existing account
        /// Validate a valid meter reading with incorrect meter read value format
        /// Successfully save a new valid meter reading
        /// Successfully update an existing meter reading with valid data
        /// </test cases>
        /// <param name="meterReading"></param>
        /// <returns></returns>
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

        /// <test cases>
        /// Validate that the method returns true for an existing account ID
        /// Validate that the method returns false for a non-existing account ID
        /// Ensure the method handles valid account IDs without exceptions
        /// </test cases>
        /// <param name="accountId"></param>
        /// <returns></returns>
        private async Task<bool> IsAccountValidAsync(int accountId)
        {
            return await _context.Users.AnyAsync(u => u.AccountID == accountId);
        }

        /// <test cases>
        /// Validate a string with exactly five digits returns true
        /// Validate a string with leading zeros and five digits returns true
        /// </test cases>
        /// <param name="meterReadValue"></param>
        /// <returns></returns>
        private bool IsMeterReadingValueValid(string meterReadValue)
        {
            //The pattern @"^\d{5}$" checks if the string is exactly five digits long.
            return Regex.IsMatch(meterReadValue, @"^\d{5}$");
        }

        /// <test cases>
        /// Successfully save a new valid meter reading
        /// Successfully update an existing meter reading with valid data
        /// Return true when changes are saved successfully
        /// </test cases>
        /// <param name="meterReading"></param>
        /// <returns></returns>
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
