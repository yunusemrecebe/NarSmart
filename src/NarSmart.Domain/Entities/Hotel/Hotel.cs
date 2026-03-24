using NarSmart.Domain.Common;
using NarSmart.Domain.ValueObjects;

namespace NarSmart.Domain.Entities.Hotel;

public class Hotel : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string Location { get; private set; } = null!;
    public Guid? ContactPersonId { get; private set; }
    public string TimeZoneId { get; private set; } = null!;
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    private Hotel() { }

    public static Hotel Create(string name, string location, string timeZoneId, DateTime startDate, DateTime? endDate = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Hotel name cannot be empty.");

        if (string.IsNullOrWhiteSpace(location))
            throw new DomainException("Hotel location cannot be empty.");

        if (string.IsNullOrWhiteSpace(timeZoneId))
            throw new DomainException("TimeZoneId is required.");

        var end = endDate ?? new DateTime(9999, 12, 31, 23, 59, 59, DateTimeKind.Utc);

        var range = new DateRange(startDate, end);

        return new Hotel
        {
            Id = Guid.NewGuid(),
            Name = name,
            Location = location,
            TimeZoneId = timeZoneId,
            StartDate = range.StartDate,
            EndDate = range.EndDate
        };
    }

    public void SetContactPerson(Guid userId)
    {
        ContactPersonId = userId;
    }

    public void Update(string name, string location, string timeZoneId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Hotel name cannot be empty.");

        Name = name;
        Location = location;
        TimeZoneId = timeZoneId;
    }
}
