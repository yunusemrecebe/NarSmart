using NarSmart.Domain.Common;
using NarSmart.Domain.Enums;

namespace NarSmart.Domain.Entities.HotelService;

public class HotelService : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public TimeOnly? ServiceStartTime { get; private set; }
    public TimeOnly? ServiceEndTime { get; private set; }
    public DayOfWeekFlag? ServiceDays { get; private set; }

    private readonly List<HotelServicePrice> _prices = new();
    public IReadOnlyCollection<HotelServicePrice> Prices => _prices.AsReadOnly();

    private readonly List<HotelServiceImage> _images = new();
    public IReadOnlyCollection<HotelServiceImage> Images => _images.AsReadOnly();

    private HotelService() { }

    public static HotelService Create(
        Guid hotelId, string name, string description,
        TimeOnly? startTime = null, TimeOnly? endTime = null,
        DayOfWeekFlag? serviceDays = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Service name cannot be empty.");

        return new HotelService
        {
            Id = Guid.NewGuid(),
            HotelId = hotelId,
            Name = name,
            Description = description,
            ServiceStartTime = startTime,
            ServiceEndTime = endTime,
            ServiceDays = serviceDays
        };
    }

    public void AddPrice(Guid currencyId, decimal amount)
    {
        if (amount < 0)
            throw new DomainException("Price amount cannot be negative.");

        if (_prices.Any(p => p.CurrencyId == currencyId))
            throw new DomainException("A price with this currency already exists.");

        _prices.Add(HotelServicePrice.Create(Id, HotelId, currencyId, amount));
    }

    public void AddImage(string imageUrl, int displayOrder)
    {
        _images.Add(HotelServiceImage.Create(Id, HotelId, imageUrl, displayOrder));
    }

    public void EnsureHasAtLeastOnePrice()
    {
        if (_prices.Count == 0)
            throw new DomainException("Service must have at least one price.");
    }
}
