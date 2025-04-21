using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satellite.Astronaut.Tracking.Models;

public class Astronaut
{
    [Key]
    public long Id { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 2)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 2)]
    public string LastName { get; set; }

    [Range(0, 50)] public int ExperienceYears { get; set; }

    public ICollection<AstronautSatellite> AstronautSatellites { get; set; }

    public Astronaut()
    {
        AstronautSatellites = new List<AstronautSatellite>();
    }

    private Astronaut(string firstName, string lastName, int experienceYears)
    {
        FirstName = firstName;
        LastName = lastName;
        ExperienceYears = experienceYears;
        AstronautSatellites = new List<AstronautSatellite>();
    }

    public static Astronaut Create(string firstName, string lastName, int experienceYears)
    {
        return new Astronaut(firstName, lastName, experienceYears);
    }

    public void AddSatellite(Satellite satellite)
    {
        if (satellite == null) throw new ArgumentNullException(nameof(satellite));
        if (satellite.Decommissioned) throw new InvalidOperationException("Cannot add a decommissioned satellite.");

        AstronautSatellites.Add(new AstronautSatellite { Satellite = satellite });
    }
}