using NarSmart.Domain.Common;

namespace NarSmart.Domain.ValueObjects;

public class PersonName : ValueObject
{
    public string FirstName { get; }
    public string LastName { get; }

    private PersonName() { FirstName = null!; LastName = null!; }

    public PersonName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainException("First name cannot be empty.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainException("Last name cannot be empty.");

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
    }

    public string FullName => $"{FirstName} {LastName}";

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }
}
