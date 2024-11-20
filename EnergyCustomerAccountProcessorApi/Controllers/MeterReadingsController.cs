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

        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<MeterReading>>> GetMeterReadings()
        {
            try
            {
                // Fetch all records from MeterReadings table
                var meterReadings = await _context.MeterReadings.ToListAsync();

                // If there are no records, return an empty list
                if (meterReadings == null || !meterReadings.Any())
                {
                    return NoContent();  // Or return Ok(meterReadings); if you prefer empty list
                }

                return Ok(meterReadings); // Return the list of meter readings
            }
            catch (Exception ex)
            {
                // Return a server error if something goes wrong
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
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
