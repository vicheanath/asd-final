using Satellite.Astronaut.Tracking.DTOs;
using Satellite.Astronaut.Tracking.Exceptions;
using Satellite.Astronaut.Tracking.Repository;

namespace Satellite.Astronaut.Tracking.Services;


public class SatelliteService : ISatelliteService
{
    private readonly ISatelliteRepository _satelliteRepository;

    public SatelliteService(ISatelliteRepository satelliteRepository)
    {
        _satelliteRepository = satelliteRepository;
    }

    public async Task<SatelliteDto> UpdateSatelliteAsync(long id, SatelliteUpdateDto dto)
    {
        var satellite = await _satelliteRepository.GetByIdAsync(id);
        if (satellite == null)
            throw new SatelliteNotFoundException(id);

        if (satellite.Decommissioned)
            throw new SatelliteDecommissionedException();

        satellite.Update(
            dto.Name,
            dto.Decommissioned
        );
        await _satelliteRepository.SaveChangesAsync();

        return new SatelliteDto
        {
            Id = satellite.Id,
            Name = satellite.Name,
            LaunchDate = satellite.LaunchDate,
            OrbitType = satellite.OrbitType,
            Decommissioned = satellite.Decommissioned
        };
    }
}