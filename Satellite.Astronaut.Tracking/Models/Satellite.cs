using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Satellite.Astronaut.Tracking.Attribute;

namespace Satellite.Astronaut.Tracking.Models;

public class Satellite
{
    [Key]
    public long Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    [Required]
    [PastDate]
    public DateTime LaunchDate { get; set; }

    [Required]
    [RegularExpression("^(LEO|MEO|GEO)$")]
    public string OrbitType { get; set; }

    public bool Decommissioned { get; set; }

    public Satellite()
    {
        Decommissioned = false;
        AstronautSatellites = new List<AstronautSatellite>();
    }

    public ICollection<AstronautSatellite> AstronautSatellites { get; set; }


    public Satellite(string name, DateTime launchDate, string orbitType)
    {
        Name = name;
        LaunchDate = launchDate;
        OrbitType = orbitType;
        AstronautSatellites = new List<AstronautSatellite>();
    }

    public static Satellite Create(string name, DateTime launchDate, string orbitType)
    {
        return new Satellite { Name = name, LaunchDate = launchDate, OrbitType = orbitType };
    }

    public void AddAstronaut(Astronaut astronaut)
    {
        if (astronaut == null) throw new ArgumentNullException(nameof(astronaut));
        if (Decommissioned) throw new InvalidOperationException("Cannot add an astronaut to a decommissioned satellite.");

        AstronautSatellites.Add(new AstronautSatellite { Astronaut = astronaut });
    }

    public void Update(string name, bool decommissioned)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        if (decommissioned && Decommissioned)
            throw new InvalidOperationException("Satellite is already decommissioned.");
        if (decommissioned && DateTime.UtcNow < LaunchDate)
            throw new InvalidOperationException("Cannot decommission a satellite that has not been launched yet.");


        Name = name;
        Decommissioned = decommissioned;
    }
}