using NarSmart.Domain.Common;

namespace NarSmart.Domain.Entities.SalesPackage;

public class SalesPackage : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string? ImageUrl { get; private set; }

    private readonly List<SalesPackagePrice> _prices = new();
    public IReadOnlyCollection<SalesPackagePrice> Prices => _prices.AsReadOnly();

    private readonly List<SalesPackageHotelService> _hotelServices = new();
    public IReadOnlyCollection<SalesPackageHotelService> HotelServices => _hotelServices.AsReadOnly();

    private SalesPackage() { }

    public static SalesPackage Create(Guid hotelId, string name, string description, string? imageUrl = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Sales package name cannot be empty.");

        return new SalesPackage
        {
            Id = Guid.NewGuid(),
            HotelId = hotelId,
            Name = name,
            Description = description,
            ImageUrl = imageUrl
        };
    }

    public void AddPrice(Guid currencyId, decimal amount)
    {
        if (amount < 0)
            throw new DomainException("Price amount cannot be negative.");

        if (_prices.Any(p => p.CurrencyId == currencyId))
            throw new DomainException("A price with this currency already exists.");

        _prices.Add(SalesPackagePrice.Create(Id, HotelId, currencyId, amount));
    }

    public void AddHotelService(Guid hotelServiceId)
    {
        if (_hotelServices.Any(hs => hs.HotelServiceId == hotelServiceId))
            throw new DomainException("This service is already included in the package.");

        _hotelServices.Add(SalesPackageHotelService.Create(Id, HotelId, hotelServiceId));
    }

    public void EnsureHasAtLeastOnePrice()
    {
        if (_prices.Count == 0)
            throw new DomainException("Sales package must have at least one price.");
    }
}
