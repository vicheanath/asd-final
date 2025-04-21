using Microsoft.EntityFrameworkCore;
using Satellite.Astronaut.Tracking;
using Satellite.Astronaut.Tracking.Data;
using Satellite.Astronaut.Tracking.Repository;
using Satellite.Astronaut.Tracking.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Add DbContext to the container.
builder.Services.AddDbContext<AppDbContext>(options =>{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});

// Ensure DbContext is registered before repositories and services
builder.Services.AddScoped<ISatelliteRepository, SatelliteRepository>();
builder.Services.AddScoped<IAstronautRepository, AstronautRepository>();
builder.Services.AddScoped<ISatelliteService, SatelliteService>();
builder.Services.AddScoped<IAstronautService, AstronautService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Add developer exception page for better debugging
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseRouting(); // Ensure routing middleware is adde

app.MapControllers(); // Map controllers to endpoints
app.Run();

