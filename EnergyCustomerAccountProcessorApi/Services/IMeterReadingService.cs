using Microsoft.AspNetCore.Http;
using EnergyCustomerAccountProcessorApi.Models;

namespace EnergyCustomerAccountProcessorApi.Services
{
    public interface IMeterReadingService
    {
        Task<IEnumerable<MeterReading>> GetAllMeterReadingsAsync();
        Task<(int SuccessCount, int FailureCount)> ProcessMeterReadingsAsync(IFormFile file);
        Task DeleteAllMeterReadingsAsync();
    }
}