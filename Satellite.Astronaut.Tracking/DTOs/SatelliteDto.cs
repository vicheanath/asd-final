namespace Satellite.Astronaut.Tracking.DTOs;

public record SatelliteDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public DateTime LaunchDate { get; set; }
    public string OrbitType { get; set; }
    public bool Decommissioned { get; set; }
}