using System.ComponentModel.DataAnnotations;

namespace Satellite.Astronaut.Tracking.DTOs;

public record AstronautCreateDto
{
    [Required]
    [StringLength(20, MinimumLength = 2)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 2)]
    public string LastName { get; set; }

    [Range(0, 50)]
    public int ExperienceYears { get; set; }

    public List<long> SatelliteIds { get; set; }
}