namespace Satellite.Astronaut.Tracking.Exceptions;

public class AstronautNotFoundException : Exception
{
    public AstronautNotFoundException(long id) : base($"Astronaut with ID {id} not found.") { }
}
