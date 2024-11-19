using System.ComponentModel.DataAnnotations;

namespace EnergyCustomerAccountProcessorApi.Models
{
    public class MeterReading
    {
        [Key]
        public int AccountId { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public decimal MeterReadValue { get; set; }
    }    
}