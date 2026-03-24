using FluentAssertions;
using NarSmart.Domain.Common;
using NarSmart.Domain.ValueObjects;

namespace NarSmart.Domain.Tests.ValueObjects;

public class PersonNameTests
{
    [Fact]
    public void Create_WithValidNames_ShouldSucceed()
    {
        var name = new PersonName("John", "Doe");

        name.FirstName.Should().Be("John");
        name.LastName.Should().Be("Doe");
    }

    [Theory]
    [InlineData("", "Doe")]
    [InlineData("John", "")]
    [InlineData(null, "Doe")]
    [InlineData("John", null)]
    public void Create_WithInvalidNames_ShouldThrow(string? firstName, string? lastName)
    {
        var act = () => new PersonName(firstName!, lastName!);

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Equals_WithSameValues_ShouldBeEqual()
    {
        var name1 = new PersonName("John", "Doe");
        var name2 = new PersonName("John", "Doe");

        name1.Should().Be(name2);
    }

    [Fact]
    public void Equals_WithDifferentValues_ShouldNotBeEqual()
    {
        var name1 = new PersonName("John", "Doe");
        var name2 = new PersonName("Jane", "Doe");

        name1.Should().NotBe(name2);
    }
}
