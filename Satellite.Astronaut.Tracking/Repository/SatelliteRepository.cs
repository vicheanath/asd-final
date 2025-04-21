using Microsoft.EntityFrameworkCore;
using Satellite.Astronaut.Tracking.Data;

namespace Satellite.Astronaut.Tracking.Repository;

public class SatelliteRepository : ISatelliteRepository
{
    private readonly AppDbContext _context;

    public SatelliteRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Models.Satellite> GetByIdAsync(long id)
    {
        return _context.Set<Models.Satellite>().FirstOrDefaultAsync(s => s.Id == id);
    }

    public Task<bool> ExistsAsync(long id)
    {
        return _context.Set<Models.Satellite>().AnyAsync(s => s.Id == id);
    }

    public Task<List<Models.Satellite>> GetByIdsAsync(IEnumerable<long> ids)
    {
        return _context.Set<Models.Satellite>().Where(s => ids.Contains(s.Id)).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}