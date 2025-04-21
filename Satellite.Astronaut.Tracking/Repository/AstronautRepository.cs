using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Satellite.Astronaut.Tracking.Data;

namespace Satellite.Astronaut.Tracking.Repository;

public class AstronautRepository : IAstronautRepository
{
    private readonly AppDbContext _context;

    public AstronautRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task AddAsync(Models.Astronaut astronaut)
    {
        _context.Set<Models.Astronaut>().Add(astronaut);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(long id)
    {
        return _context.Set<Models.Astronaut>().AnyAsync(a => a.Id == id);
    }

    public IQueryable<Models.Astronaut> GetAllIncluding(params Expression<Func<Models.Astronaut, object>>[] includes)
    {
        IQueryable<Models.Astronaut> query = _context.Set<Models.Astronaut>();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}