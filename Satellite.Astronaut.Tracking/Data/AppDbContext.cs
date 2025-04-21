using Microsoft.EntityFrameworkCore;
using Satellite.Astronaut.Tracking.Models;

namespace Satellite.Astronaut.Tracking.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Models.Astronaut> Astronauts { get; set; }
    public DbSet<Models.Satellite> Satellites { get; set; }
    public DbSet<AstronautSatellite> AstronautSatellites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Astronaut>()
            .Property(a => a.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<Models.Satellite>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<AstronautSatellite>()
            .HasKey(x => new { x.AstronautId, x.SatelliteId });

        modelBuilder.Entity<AstronautSatellite>()
            .HasOne(x => x.Astronaut)
            .WithMany(a => a.AstronautSatellites)
            .HasForeignKey(x => x.AstronautId);

        modelBuilder.Entity<AstronautSatellite>()
            .HasOne(x => x.Satellite)
            .WithMany(s => s.AstronautSatellites)
            .HasForeignKey(x => x.SatelliteId);


        modelBuilder.Entity<Models.Astronaut>().HasData(
            new Models.Astronaut { Id = -1, FirstName = "Neil", LastName = "Armstrong", ExperienceYears = 12 },
            new Models.Astronaut { Id = -2, FirstName = "Sally", LastName = "Ride", ExperienceYears = 8 },
            new Models.Astronaut { Id = -3, FirstName = "Chris", LastName = "Hadfield", ExperienceYears = 15 }
        );

        modelBuilder.Entity<Models.Satellite>().HasData(
            new Models.Satellite { Id = 1, Name = "Hubble", LaunchDate = new DateTime(1990, 4, 24), OrbitType = "LEO" },
            new Models.Satellite { Id = 2, Name = "Starlink-17", LaunchDate = new DateTime(2023, 8, 14), OrbitType = "MEO" },
            new Models.Satellite { Id = 3, Name = "Sentinel-6", LaunchDate = new DateTime(2020, 11, 21), OrbitType = "LEO" }
        );

        modelBuilder.Entity<AstronautSatellite>().HasData(
            new AstronautSatellite { AstronautId = -1, SatelliteId = 1 },
            new AstronautSatellite { AstronautId = -2, SatelliteId = 2 },
            new AstronautSatellite { AstronautId = -3, SatelliteId = 3 }
        );
    }
}