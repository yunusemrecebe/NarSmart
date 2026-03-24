using NarSmart.Domain.Common;

namespace NarSmart.Domain.Entities.Currency;

public class Currency : SystemEntity
{
    public string Code { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public string Symbol { get; private set; } = null!;

    private Currency() { }

    public static Currency Create(string code, string name, string symbol)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new DomainException("Currency code cannot be empty.");

        if (code.Length != 3)
            throw new DomainException("Currency code must be exactly 3 characters.");

        return new Currency
        {
            Id = Guid.NewGuid(),
            Code = code.ToUpperInvariant(),
            Name = name,
            Symbol = symbol
        };
    }
}
