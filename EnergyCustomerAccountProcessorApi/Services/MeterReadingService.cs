using CsvHelper;
using CsvHelper.Configuration;
using EnergyCustomerAccountProcessorApi.Data;
using EnergyCustomerAccountProcessorApi.Models;
using EnergyCustomerAccountProcessorApi.Validation;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EnergyCustomerAccountProcessorApi.Services
{
    public class MeterReadingService : IMeterReadingService
    {
        private readonly EnergyContext _context;
        private readonly IMeterReadingValidator _validator;

        public MeterReadingService(EnergyContext context, IMeterReadingValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<IEnumerable<MeterReading>> GetAllMeterReadingsAsync()
        {
            try
            {
                var meterReadings = await _context.MeterReadings.ToListAsync();
                if (meterReadings == null || !meterReadings.Any())
                {
                    throw new InvalidOperationException("No meter readings found.");
                }
                return meterReadings;
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Error fetching meter readings from the database.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error fetching meter readings: {ex.Message}", ex);
            }
        }

        public async Task<(int SuccessCount, int FailureCount)> ProcessMeterReadingsAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File cannot be null or empty.");
            }

            try
            {
                using var stream = file.OpenReadStream();
                using var reader = new StreamReader(stream);
                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    PrepareHeaderForMatch = args => args.Header.ToLower(), // Case-insensitive headers
                });

                // Custom date converter for MeterReadingDateTime field
                csv.Context.TypeConverterOptionsCache.GetOptions<DateTime>().Formats = new[] { "dd/MM/yyyy HH:mm" };

                var records = csv.GetRecords<MeterReading>().ToList();

                int successCount = 0, failureCount = 0;

                foreach (var record in records)
                {                    
                    if (await _validator.ValidateMeterReadingAsync(record))
                    {
                        successCount++;
                    }
                    else
                    {
                        failureCount++; 
                    }
                }
                return (successCount, failureCount);
            }
            catch (CsvHelperException ex)
            {
                throw new ApplicationException("Error reading the CSV file.", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Error saving meter readings to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An unexpected error occurred while processing the meter readings: {ex.Message}", ex);
            }
        }

        public async Task DeleteAllMeterReadingsAsync()
        {
            try
            {
                var meterReadings = await _context.MeterReadings.ToListAsync();
                if (meterReadings == null || !meterReadings.Any())
                {
                    throw new InvalidOperationException("No meter readings to delete.");
                }

                _context.MeterReadings.RemoveRange(meterReadings);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Error deleting meter readings from the database.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while deleting meter readings: {ex.Message}", ex);
            }
        }
    }
}
