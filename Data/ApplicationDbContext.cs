using Microsoft.EntityFrameworkCore;
using SumCalculator.Data.Configurations;

namespace SumCalculator.Data
{
    public class ApplicatonDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CalculatorEntryConfiguration());
        }
    }
}