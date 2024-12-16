using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SumCalculator.Models;

namespace SumCalculator.Data.Configurations;

public class CalculatorEntryConfiguration : IEntityTypeConfiguration<CalculatorRecord>
{
    public void Configure(EntityTypeBuilder<CalculatorRecord> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder
        .Property(x => x.Value1)
        .HasColumnName("value1")
        .HasColumnType("decimal(18, 6)")
        .IsRequired(true);

         builder
        .Property(x => x.Value2)
        .HasColumnName("value2")
        .HasColumnType("decimal(18, 6)")
        .IsRequired(true);

         builder
        .Property(x => x.Sum)
        .HasColumnName("sum")
        .HasComputedColumnSql("[value1] + [value2]", stored : true)
        .HasColumnType("decimal(18, 6)")
        .IsRequired(true);

         builder
        .Property(x => x.DateTime)
        .HasColumnName("date")
        .HasDefaultValueSql("getdate()")
        .IsRequired(true);

        builder.ToTable("calculation_records");

    }
}