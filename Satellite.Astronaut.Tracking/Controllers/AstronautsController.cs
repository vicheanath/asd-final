using Microsoft.AspNetCore.Mvc;
using Satellite.Astronaut.Tracking.DTOs;
using Satellite.Astronaut.Tracking.Services;

namespace Satellite.Astronaut.Tracking.Controllers;

[ApiController]
[Route("api/v1/astronauts")]
public class AstronautsController : ControllerBase
{
    private readonly IAstronautService _astronautService;

    public AstronautsController(IAstronautService astronautService)
    {
        _astronautService = astronautService;
    }

    [HttpPost]
    public async Task<ActionResult<AstronautDto>> CreateAstronaut(AstronautCreateDto dto)
    {
        var astronaut = await _astronautService.CreateAstronautAsync(dto);
        return Ok(astronaut);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AstronautDto>>> GetAstronauts(
        [FromQuery] string sort = "experienceYears", [FromQuery] string order = "asc")
    {
        var astronauts = await _astronautService.GetAllAstronautsAsync(sort, order);
        return Ok(astronauts);
    }
}
