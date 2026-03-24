using NarSmart.Domain.Common;

namespace NarSmart.Domain.ValueObjects;

public class DateRange : ValueObject
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    private DateRange() { }

    public DateRange(DateTime startDate, DateTime endDate)
    {
        if (endDate < startDate)
            throw new DomainException("End date cannot be before start date.");

        StartDate = startDate;
        EndDate = endDate;
    }

    public bool Overlaps(DateRange other)
    {
        return StartDate < other.EndDate && other.StartDate < EndDate;
    }

    public bool Contains(DateTime date)
    {
        return date >= StartDate && date <= EndDate;
    }

    public int TotalDays => (EndDate - StartDate).Days;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return StartDate;
        yield return EndDate;
    }
}
