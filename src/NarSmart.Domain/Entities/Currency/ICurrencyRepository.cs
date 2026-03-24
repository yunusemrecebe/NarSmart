namespace NarSmart.Domain.Entities.Currency;

public interface ICurrencyRepository
{
    Task<Currency?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Currency?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<List<Currency>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Currency currency, CancellationToken cancellationToken = default);
}
