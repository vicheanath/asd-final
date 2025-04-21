using System.Linq.Expressions;

namespace Satellite.Astronaut.Tracking.Repository;

public interface IAstronautRepository
{
    Task AddAsync(Models.Astronaut astronaut);
    Task<bool> ExistsAsync(long id);
    IQueryable<Models.Astronaut> GetAllIncluding(params Expression<Func<Models.Astronaut, object>>[] includes);
    Task SaveChangesAsync();
}