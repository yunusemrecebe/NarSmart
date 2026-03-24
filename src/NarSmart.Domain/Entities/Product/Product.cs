using NarSmart.Domain.Common;

namespace NarSmart.Domain.Entities.Product;

public class Product : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }

    private readonly List<ProductPrice> _prices = new();
    public IReadOnlyCollection<ProductPrice> Prices => _prices.AsReadOnly();

    private Product() { }

    public static Product Create(Guid hotelId, string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name cannot be empty.");

        return new Product
        {
            Id = Guid.NewGuid(),
            HotelId = hotelId,
            Name = name,
            Description = description
        };
    }

    public void AddPrice(Guid currencyId, decimal amount)
    {
        if (amount < 0)
            throw new DomainException("Price amount cannot be negative.");

        if (_prices.Any(p => p.CurrencyId == currencyId))
            throw new DomainException("A price with this currency already exists.");

        _prices.Add(ProductPrice.Create(Id, HotelId, currencyId, amount));
    }

    public void EnsureHasAtLeastOnePrice()
    {
        if (_prices.Count == 0)
            throw new DomainException("Product must have at least one price.");
    }
}
