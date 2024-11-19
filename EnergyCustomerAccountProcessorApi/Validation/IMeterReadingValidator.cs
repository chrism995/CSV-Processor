using EnergyCustomerAccountProcessorApi.Data;
using EnergyCustomerAccountProcessorApi.Models;

namespace EnergyCustomerAccountProcessorApi.Validation
{
    public interface IMeterReadingValidator
    {
        Task<bool> ValidateMeterReadingAsync(MeterReading meterReading);
    }
}