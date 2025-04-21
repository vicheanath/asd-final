using Microsoft.AspNetCore.Mvc;
using Satellite.Astronaut.Tracking.DTOs;
using Satellite.Astronaut.Tracking.Services;

namespace Satellite.Astronaut.Tracking.Controllers;


[ApiController]
[Route("api/v1/satellites")]
public class SatellitesController : ControllerBase
{
    private readonly ISatelliteService _satelliteService;

    public SatellitesController(ISatelliteService satelliteService)
    {
        _satelliteService = satelliteService;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SatelliteDto>> UpdateSatellite(long id, SatelliteUpdateDto dto)
    {
        var satellite = await _satelliteService.UpdateSatelliteAsync(id, dto);
        return Ok(satellite);
    }
}