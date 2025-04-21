using Microsoft.EntityFrameworkCore;
using Satellite.Astronaut.Tracking.DTOs;
using Satellite.Astronaut.Tracking.Exceptions;
using Satellite.Astronaut.Tracking.Models;
using Satellite.Astronaut.Tracking.Repository;

namespace Satellite.Astronaut.Tracking.Services;

public class AstronautService : IAstronautService
{
    private readonly IAstronautRepository _astronautRepository;
    private readonly ISatelliteRepository _satelliteRepository;

    public AstronautService(IAstronautRepository astronautRepository, ISatelliteRepository satelliteRepository)
    {
        _astronautRepository = astronautRepository;
        _satelliteRepository = satelliteRepository;
    }

    public async Task<AstronautDto> CreateAstronautAsync(AstronautCreateDto dto)
    {
        foreach (var satelliteId in dto.SatelliteIds)
        {
            if (!await _satelliteRepository.ExistsAsync(satelliteId))
                throw new SatelliteNotFoundException(satelliteId);
        }

        var astronaut = Models.Astronaut.Create(dto.FirstName, dto.LastName, dto.ExperienceYears);
        var satellites = await _satelliteRepository.GetByIdsAsync(dto.SatelliteIds);
        astronaut.AstronautSatellites = satellites.Select(s => new AstronautSatellite { Satellite = s }).ToList();

        await _astronautRepository.AddAsync(astronaut);
        await _astronautRepository.SaveChangesAsync();
        return new AstronautDto
        {
            Id = astronaut.Id,
            FirstName = astronaut.FirstName,
            LastName = astronaut.LastName,
            ExperienceYears = astronaut.ExperienceYears,
            Satellites = astronaut.AstronautSatellites.Select(x => new SatelliteDto
            {
                Id = x.Satellite.Id,
                Name = x.Satellite.Name,
                LaunchDate = x.Satellite.LaunchDate,
                OrbitType = x.Satellite.OrbitType,
                Decommissioned = x.Satellite.Decommissioned
            }).ToList()
        };
    }

    public async Task<IEnumerable<AstronautDto>> GetAllAstronautsAsync(string sort, string order)
    {
        var query = _astronautRepository
            .GetAllIncluding(a => a.AstronautSatellites, a => a.AstronautSatellites.Select(x => x.Satellite))
            .AsQueryable();
        

        query = (sort.ToLower(), order.ToLower()) switch
        {
            ("experienceyears", "desc") => query.OrderByDescending(a => a.ExperienceYears),
            ("experienceyears", _) => query.OrderBy(a => a.ExperienceYears),
            _ => query
        };

        var astronauts = await query.ToListAsync();
        return astronauts.Select(a => new AstronautDto
        {
            Id = a.Id,
            FirstName = a.FirstName,
            LastName = a.LastName,
            ExperienceYears = a.ExperienceYears,
            Satellites = a.AstronautSatellites.Select(x => new SatelliteDto
            {
                Id = x.Satellite.Id,
                Name = x.Satellite.Name,
                LaunchDate = x.Satellite.LaunchDate,
                OrbitType = x.Satellite.OrbitType,
                Decommissioned = x.Satellite.Decommissioned
            }).ToList()
        });
    }
}
