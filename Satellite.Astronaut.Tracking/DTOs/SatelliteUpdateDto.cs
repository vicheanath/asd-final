using System.ComponentModel.DataAnnotations;

namespace Satellite.Astronaut.Tracking.DTOs;

public record SatelliteUpdateDto
{
    [StringLength(255)]
    public string Name { get; set; }
    public bool Decommissioned { get; set; }
}