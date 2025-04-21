namespace Satellite.Astronaut.Tracking.Exceptions;

public class SatelliteNotFoundException : Exception
{
    public SatelliteNotFoundException(long id) : base($"Satellite with ID {id} not found.") { }
}