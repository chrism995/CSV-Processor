using EnergyCustomerAccountProcessorApi.Data;
using EnergyCustomerAccountProcessorApi.Models;
using EnergyCustomerAccountProcessorApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using System.Globalization;

namespace EnergyCustomerAccountProcessorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingsController : ControllerBase
    {
        private readonly EnergyContext _context;
        private readonly IMeterReadingService _meterReadingService;

        public MeterReadingsController(EnergyContext context,IMeterReadingService meterReadingService)
        {
            _context = context;
            _meterReadingService = meterReadingService;
        }

        // POST: api/Energy/UploadMeterReadings
        [HttpPost("meter-reading-uploads")]
        public async Task<IActionResult> ProcessMeterReadings(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("A valid CSV file must be provided.");
            }

            var (successCount, failureCount) = await _meterReadingService.ProcessMeterReadingsAsync(file);

            return Ok(new
            {
                SuccessCount = successCount,
                FailureCount = failureCount
            });
        }
    }
}
