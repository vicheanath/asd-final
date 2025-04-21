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

    public async Task<IQueryable<Models.Astronaut>> GetAllIncludingAsync(params Expression<Func<Models.Astronaut, object>>[] includes)
    {
        IQueryable<Models.Astronaut> query = _context.Set<Models.Astronaut>();

        foreach (var include in includes)
        {
            if (include.Body is MemberExpression || include.Body is UnaryExpression)
            {
                query = query.Include(include);
            }
            else
            {
                throw new InvalidOperationException("Invalid Include expression. Only direct property access is supported.");
            }
        }

        return await Task.FromResult(query);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}