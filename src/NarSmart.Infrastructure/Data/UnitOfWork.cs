using NarSmart.Domain.Common;

namespace NarSmart.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly NarSmartDbContext _context;

    public UnitOfWork(NarSmartDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
