using CsvHelper;
using CsvHelper.Configuration;
using EnergyCustomerAccountProcessorApi.Data;
using EnergyCustomerAccountProcessorApi.Models;
using EnergyCustomerAccountProcessorApi.Validation;
using Microsoft.AspNetCore.Http;
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

        public async Task<(int SuccessCount, int FailureCount)> ProcessMeterReadingsAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File cannot be null or empty.");
            }

            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                PrepareHeaderForMatch = args => args.Header.ToLower(), // Case-insensitive headers
            });

            // Register custom date converter for MeterReadingDateTime field
            csv.Context.TypeConverterOptionsCache.GetOptions<DateTime>().Formats = new[] { "dd/MM/yyyy HH:mm" };

            var records = csv.GetRecords<MeterReading>().ToList();

            int successCount = 0, failureCount = 0;

            foreach (var record in records)
            {
                // Validate each record
                if (await _validator.ValidateMeterReadingAsync(record))
                {
                    var existingMeterReading = await _context.MeterReadings
                        .FirstOrDefaultAsync(m => m.AccountId == record.AccountId);

                    if (existingMeterReading == null)
                    {
                        // Add new record if it doesn't exist
                        _context.MeterReadings.Add(record);
                        successCount++; 
                    }
                    else if (record.MeterReadingDateTime > existingMeterReading.MeterReadingDateTime)
                    {
                        
                        existingMeterReading.MeterReadValue = record.MeterReadValue;
                        existingMeterReading.MeterReadingDateTime = record.MeterReadingDateTime;
                        _context.MeterReadings.Update(existingMeterReading);
                        successCount++; 
                    }
                    else
                    {
                        // If the new date is older, treat it as a failure
                        failureCount++;
                    }
                }
                else                                                                                                                    
                {
                    failureCount++;
                }
            }

            await _context.SaveChangesAsync();
            return (successCount, failureCount);
        }

        public async Task DeleteAllMeterReadingsAsync()
        {
            var meterReadings = await _context.MeterReadings.ToListAsync();
            _context.MeterReadings.RemoveRange(meterReadings);
            await _context.SaveChangesAsync();
        }
    }    
}