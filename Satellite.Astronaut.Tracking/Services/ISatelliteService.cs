using Satellite.Astronaut.Tracking.DTOs;

namespace Satellite.Astronaut.Tracking.Services;

public interface ISatelliteService
{
    Task<SatelliteDto> UpdateSatelliteAsync(long id, SatelliteUpdateDto dto);
}