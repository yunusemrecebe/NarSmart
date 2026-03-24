using Microsoft.EntityFrameworkCore;
using NarSmart.Domain.Entities.Sale;

namespace NarSmart.Infrastructure.Data.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly NarSmartDbContext _context;

    public SaleRepository(NarSmartDbContext context) => _context = context;

    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Sales
            .Include(s => s.SaleCustomers)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public async Task<List<Sale>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _context.Sales
            .Include(s => s.SaleCustomers)
            .ToListAsync(cancellationToken);

    public async Task<bool> HasOverlappingReservationAsync(
        Guid hotelId, Guid roomId, DateTime startDate, DateTime endDate,
        CancellationToken cancellationToken = default)
        => await _context.Sales.AnyAsync(
            s => s.RoomId == roomId && s.StartDate < endDate && startDate < s.EndDate,
            cancellationToken);

    public async Task AddAsync(Sale sale, CancellationToken cancellationToken = default)
        => await _context.Sales.AddAsync(sale, cancellationToken);

    public void Update(Sale sale) => _context.Sales.Update(sale);
}
