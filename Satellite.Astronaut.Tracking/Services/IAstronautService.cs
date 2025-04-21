using Satellite.Astronaut.Tracking.DTOs;

namespace Satellite.Astronaut.Tracking.Services;

public interface IAstronautService
{
    public Task<AstronautDto> CreateAstronautAsync(AstronautCreateDto dto);
    Task<IEnumerable<AstronautDto>> GetAllAstronautsAsync(string sort, string order);
}