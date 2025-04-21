namespace Satellite.Astronaut.Tracking.Models;

public class AstronautSatellite
{
    public long AstronautId { get; set; }
    public Astronaut Astronaut { get; set; } = null!; // Ensure non-nullable reference
    public long SatelliteId { get; set; }
    public Satellite Satellite { get; set; } = null!; // Ensure non-nullable reference
}