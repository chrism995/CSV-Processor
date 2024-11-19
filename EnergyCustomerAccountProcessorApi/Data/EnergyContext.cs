using Microsoft.EntityFrameworkCore;
using EnergyCustomerAccountProcessorApi.Models;

namespace EnergyCustomerAccountProcessorApi.Data
{
    public class EnergyContext : DbContext
    {
        public EnergyContext(DbContextOptions<EnergyContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<MeterReading> MeterReadings { get; set; }
    }
}
