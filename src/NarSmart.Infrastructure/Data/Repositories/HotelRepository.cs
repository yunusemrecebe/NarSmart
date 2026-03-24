using Microsoft.EntityFrameworkCore;
using NarSmart.Domain.Entities.Hotel;

namespace NarSmart.Infrastructure.Data.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly NarSmartDbContext _context;

    public HotelRepository(NarSmartDbContext context) => _context = context;

    public async Task<Hotel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

    public async Task AddAsync(Hotel hotel, CancellationToken cancellationToken = default)
        => await _context.Hotels.AddAsync(hotel, cancellationToken);

    public void Update(Hotel hotel) => _context.Hotels.Update(hotel);
}
