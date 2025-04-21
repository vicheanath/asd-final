namespace Satellite.Astronaut.Tracking.Repository;

public interface ISatelliteRepository
{
    Task<Models.Satellite> GetByIdAsync(long id);
    Task<bool> ExistsAsync(long id);
    Task<List<Models.Satellite>> GetByIdsAsync(IEnumerable<long> ids);
    Task SaveChangesAsync();
}