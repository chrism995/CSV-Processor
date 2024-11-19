using System.ComponentModel.DataAnnotations;

namespace EnergyCustomerAccountProcessorApi.Models
{
    public class User
    {
        [Key]
        public int AccountID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<MeterReading> MeterReadings { get; set; }
    }    
}