namespace Satellite.Astronaut.Tracking.Exceptions;

public class SatelliteDecommissionedException : Exception
{
    public SatelliteDecommissionedException() : base("Cannot update a decommissioned satellite.") { }
}