using Microsoft.AspNetCore.Http;

namespace EnergyCustomerAccountProcessorApi.Services
{
    public interface IMeterReadingService
    {
        Task<(int SuccessCount, int FailureCount)> ProcessMeterReadingsAsync(IFormFile file);
    }
}