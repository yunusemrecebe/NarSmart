using FluentAssertions;
using NarSmart.Domain.Common;
using NarSmart.Domain.ValueObjects;

namespace NarSmart.Domain.Tests.ValueObjects;

public class DateRangeTests
{
    [Fact]
    public void Create_WithValidDates_ShouldSucceed()
    {
        var start = DateTime.UtcNow;
        var end = start.AddDays(5);

        var range = new DateRange(start, end);

        range.StartDate.Should().Be(start);
        range.EndDate.Should().Be(end);
    }

    [Fact]
    public void Create_EndBeforeStart_ShouldThrow()
    {
        var start = DateTime.UtcNow;
        var end = start.AddDays(-1);

        var act = () => new DateRange(start, end);

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Overlaps_OverlappingRanges_ShouldReturnTrue()
    {
        var range1 = new DateRange(new DateTime(2025, 1, 1), new DateTime(2025, 1, 10));
        var range2 = new DateRange(new DateTime(2025, 1, 5), new DateTime(2025, 1, 15));

        range1.Overlaps(range2).Should().BeTrue();
    }

    [Fact]
    public void Overlaps_NonOverlappingRanges_ShouldReturnFalse()
    {
        var range1 = new DateRange(new DateTime(2025, 1, 1), new DateTime(2025, 1, 5));
        var range2 = new DateRange(new DateTime(2025, 1, 10), new DateTime(2025, 1, 15));

        range1.Overlaps(range2).Should().BeFalse();
    }

    [Fact]
    public void TotalDays_ShouldCalculateCorrectly()
    {
        var range = new DateRange(new DateTime(2025, 1, 1), new DateTime(2025, 1, 6));

        range.TotalDays.Should().Be(5);
    }
}
