using System.Linq.Expressions;

namespace Satellite.Astronaut.Tracking.Repository;

public interface IAstronautRepository
{
    Task AddAsync(Models.Astronaut astronaut);
    Task<bool> ExistsAsync(long id);
    Task<IQueryable<Models.Astronaut>> GetAllIncludingAsync(params Expression<Func<Models.Astronaut, object>>[] includes);
    Task SaveChangesAsync();
}