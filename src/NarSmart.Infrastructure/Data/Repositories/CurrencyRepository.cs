using Microsoft.EntityFrameworkCore;
using NarSmart.Domain.Entities.Currency;

namespace NarSmart.Infrastructure.Data.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly NarSmartDbContext _context;

    public CurrencyRepository(NarSmartDbContext context) => _context = context;

    public async Task<Currency?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Currencies.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task<Currency?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        => await _context.Currencies.FirstOrDefaultAsync(c => c.Code == code, cancellationToken);

    public async Task<List<Currency>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _context.Currencies.ToListAsync(cancellationToken);

    public async Task AddAsync(Currency currency, CancellationToken cancellationToken = default)
        => await _context.Currencies.AddAsync(currency, cancellationToken);
}
