using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NarSmart.Domain.Common;
using NarSmart.Domain.Entities.Currency;
using NarSmart.Domain.Entities.Customer;
using NarSmart.Domain.Entities.Hotel;
using NarSmart.Domain.Entities.Room;
using NarSmart.Domain.Entities.Sale;
using NarSmart.Infrastructure.Data;
using NarSmart.Infrastructure.Data.Repositories;
using NarSmart.Infrastructure.Identity;

namespace NarSmart.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NarSmartDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<NarSmartDbContext>()
        .AddDefaultTokenProviders();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();

        return services;
    }
}
