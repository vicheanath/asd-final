using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Satellite.Astronaut.Tracking.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Astronauts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ExperienceYears = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Astronauts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Satellites",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LaunchDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OrbitType = table.Column<string>(type: "text", nullable: false),
                    Decommissioned = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satellites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AstronautSatellites",
                columns: table => new
                {
                    AstronautId = table.Column<long>(type: "bigint", nullable: false),
                    SatelliteId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AstronautSatellites", x => new { x.AstronautId, x.SatelliteId });
                    table.ForeignKey(
                        name: "FK_AstronautSatellites_Astronauts_AstronautId",
                        column: x => x.AstronautId,
                        principalTable: "Astronauts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AstronautSatellites_Satellites_SatelliteId",
                        column: x => x.SatelliteId,
                        principalTable: "Satellites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Astronauts",
                columns: new[] { "Id", "ExperienceYears", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1L, 12, "Neil", "Armstrong" },
                    { 2L, 8, "Sally", "Ride" },
                    { 3L, 15, "Chris", "Hadfield" }
                });

            migrationBuilder.InsertData(
                table: "Satellites",
                columns: new[] { "Id", "Decommissioned", "LaunchDate", "Name", "OrbitType" },
                values: new object[,]
                {
                    { 1L, false, new DateTime(1990, 4, 24, 5, 0, 0, 0, DateTimeKind.Utc), "Hubble", "LEO" },
                    { 2L, false, new DateTime(2023, 8, 14, 5, 0, 0, 0, DateTimeKind.Utc), "Starlink-17", "MEO" },
                    { 3L, false, new DateTime(2020, 11, 21, 6, 0, 0, 0, DateTimeKind.Utc), "Sentinel-6", "LEO" }
                });

            migrationBuilder.InsertData(
                table: "AstronautSatellites",
                columns: new[] { "AstronautId", "SatelliteId" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 2L, 2L },
                    { 3L, 3L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AstronautSatellites_SatelliteId",
                table: "AstronautSatellites",
                column: "SatelliteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AstronautSatellites");

            migrationBuilder.DropTable(
                name: "Astronauts");

            migrationBuilder.DropTable(
                name: "Satellites");
        }
    }
}
