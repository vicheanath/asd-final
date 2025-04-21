namespace Satellite.Astronaut.Tracking.DTOs;

public record AstronautDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int ExperienceYears { get; set; }
    public List<SatelliteDto> Satellites { get; set; }
}