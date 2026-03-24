using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NarSmart.Domain.Entities.Currency;
using NarSmart.Domain.Entities.Hotel;
using NarSmart.Infrastructure.Identity;

namespace NarSmart.Infrastructure.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NarSmartDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        await context.Database.MigrateAsync();

        await SeedRolesAsync(roleManager);
        await SeedCurrenciesAsync(context);
        var hotel = await SeedHotelAsync(context);
        await SeedSystemAdminAsync(userManager, context, hotel.Id);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
    {
        string[] roles = { "SystemAdmin", "Manager", "Receptionist", "Waiter" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
        }
    }

    private static async Task SeedCurrenciesAsync(NarSmartDbContext context)
    {
        if (await context.Currencies.AnyAsync()) return;

        var currencies = new[]
        {
            Currency.Create("TRY", "Turkish Lira", "₺"),
            Currency.Create("USD", "US Dollar", "$"),
            Currency.Create("EUR", "Euro", "€")
        };

        await context.Currencies.AddRangeAsync(currencies);
        await context.SaveChangesAsync();
    }

    private static async Task<Hotel> SeedHotelAsync(NarSmartDbContext context)
    {
        var existing = await context.Hotels.IgnoreQueryFilters().FirstOrDefaultAsync();
        if (existing is not null) return existing;

        var hotel = Hotel.Create("NarSmart Demo Hotel", "Istanbul, Turkey",
            "Europe/Istanbul", DateTime.UtcNow);
        await context.Hotels.AddAsync(hotel);
        await context.SaveChangesAsync();
        return hotel;
    }

    private static async Task SeedSystemAdminAsync(
        UserManager<ApplicationUser> userManager,
        NarSmartDbContext context,
        Guid hotelId)
    {
        const string adminEmail = "admin@narsmart.com";

        var existingUser = await userManager.FindByEmailAsync(adminEmail);
        if (existingUser is not null) return;

        var adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FirstName = "System",
            LastName = "Admin",
            RegistrationNumber = "SYS-001",
            HotelId = hotelId,
            EmailConfirmed = true,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        var result = await userManager.CreateAsync(adminUser, "Admin123!");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "SystemAdmin");

            var userHotel = UserHotel.Create(adminUser.Id, hotelId);
            await context.UserHotels.AddAsync(userHotel);
            await context.SaveChangesAsync();
        }
    }
}
