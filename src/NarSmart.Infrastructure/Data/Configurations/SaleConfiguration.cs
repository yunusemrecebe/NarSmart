using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NarSmart.Domain.Entities.Sale;

namespace NarSmart.Infrastructure.Data.Configurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");
        builder.HasKey(s => s.Id);

        builder.HasMany(s => s.SaleCustomers)
            .WithOne()
            .HasForeignKey(sc => sc.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class SaleCustomerConfiguration : IEntityTypeConfiguration<SaleCustomer>
{
    public void Configure(EntityTypeBuilder<SaleCustomer> builder)
    {
        builder.ToTable("SaleCustomers");
        builder.HasKey(sc => sc.Id);
        builder.HasIndex(sc => new { sc.SaleId, sc.CustomerId }).IsUnique();
    }
}
