using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NarSmart.Application.Common.Interfaces;
using NarSmart.Domain.Common;
using NarSmart.Domain.Entities.Currency;
using NarSmart.Domain.Entities.Customer;
using NarSmart.Domain.Entities.Discount;
using NarSmart.Domain.Entities.Hotel;
using NarSmart.Domain.Entities.HotelService;
using NarSmart.Domain.Entities.Product;
using NarSmart.Domain.Entities.Room;
using NarSmart.Domain.Entities.Sale;
using NarSmart.Domain.Entities.SalesPackage;
using NarSmart.Infrastructure.Identity;

namespace NarSmart.Infrastructure.Data;

public class NarSmartDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    private readonly ICurrentTenantService? _tenantService;

    public NarSmartDbContext(DbContextOptions<NarSmartDbContext> options, ICurrentTenantService? tenantService = null)
        : base(options)
    {
        _tenantService = tenantService;
    }

    public DbSet<Hotel> Hotels => Set<Hotel>();
    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<HotelService> HotelServices => Set<HotelService>();
    public DbSet<HotelServicePrice> HotelServicePrices => Set<HotelServicePrice>();
    public DbSet<HotelServiceImage> HotelServiceImages => Set<HotelServiceImage>();
    public DbSet<SalesPackage> SalesPackages => Set<SalesPackage>();
    public DbSet<SalesPackagePrice> SalesPackagePrices => Set<SalesPackagePrice>();
    public DbSet<SalesPackageHotelService> SalesPackageHotelServices => Set<SalesPackageHotelService>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductPrice> ProductPrices => Set<ProductPrice>();
    public DbSet<Discount> Discounts => Set<Discount>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleCustomer> SaleCustomers => Set<SaleCustomer>();
    public DbSet<UserHotel> UserHotels => Set<UserHotel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NarSmartDbContext).Assembly);

        ApplyGlobalQueryFilters(modelBuilder);
    }

    private void ApplyGlobalQueryFilters(ModelBuilder modelBuilder)
    {
        if (_tenantService is null) return;

        var currentHotelId = _tenantService.HotelId;

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(NarSmartDbContext)
                    .GetMethod(nameof(ApplyTenantFilter),
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
                    .MakeGenericMethod(entityType.ClrType);

                method.Invoke(null, new object[] { modelBuilder, currentHotelId });
            }
        }
    }

    private static void ApplyTenantFilter<T>(ModelBuilder modelBuilder, Guid hotelId) where T : BaseEntity
    {
        modelBuilder.Entity<T>().HasQueryFilter(e => e.HotelId == hotelId && !e.IsDeleted);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (entry.Entity.HotelId == Guid.Empty && _tenantService is not null && _tenantService.HotelId != Guid.Empty)
                        entry.Entity.HotelId = _tenantService.HotelId;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    if (_tenantService is not null && _tenantService.UserId != Guid.Empty)
                        entry.Entity.CreatedBy = _tenantService.UserId.ToString();
                    entry.Entity.IsActive = true;
                    entry.Entity.IsDeleted = false;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    if (_tenantService is not null)
                        entry.Entity.UpdatedBy = _tenantService.UserId.ToString();
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.IsActive = false;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    if (_tenantService is not null)
                        entry.Entity.UpdatedBy = _tenantService.UserId.ToString();
                    break;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SystemEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.IsActive = true;
                    entry.Entity.IsDeleted = false;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
