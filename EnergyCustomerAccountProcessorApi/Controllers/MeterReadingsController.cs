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
        private readonly IMeterReadingService _meterReadingService;

        public MeterReadingsController(IMeterReadingService meterReadingService)
        {
            _meterReadingService = meterReadingService;
        }

         [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<MeterReading>>> GetMeterReadings()
        {
            try
            {
                var meterReadings = await _meterReadingService.GetAllMeterReadingsAsync();

                if (meterReadings == null || !meterReadings.Any())
                {
                    return NoContent();
                }

                return Ok(meterReadings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpPost("meter-reading-uploads")]
        public async Task<IActionResult> ProcessMeterReadings(IFormFile file)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the meter readings: {ex.Message}");
            }
        }

        [HttpDelete("delete-all")]
        public async Task<IActionResult> DeleteAllMeterReadings()
        {
            try
            {
                await _meterReadingService.DeleteAllMeterReadingsAsync();
                return Ok("All meter readings have been deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
