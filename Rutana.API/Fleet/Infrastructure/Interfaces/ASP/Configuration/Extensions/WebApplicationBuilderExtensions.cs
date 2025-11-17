using Rutana.API.Fleet.Application.Internal.CommandServices;
using Rutana.API.Fleet.Application.Internal.QueryServices;
using Rutana.API.Fleet.Domain.Repositories;
using Rutana.API.Fleet.Domain.Services;
using Rutana.API.Fleet.Infrastructure.Persistence.EFC.Repositories;

namespace Rutana.API.Fleet.Infrastructure.Interfaces.ASP.Configuration.Extensions;

/// <summary>
/// Web application builder extensions for Fleet bounded context.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Adds Fleet context services to the application.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    public static void AddFleetContextServices(this WebApplicationBuilder builder)
    {
        // Fleet Bounded Context - Repositories
        builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();

        // Fleet Bounded Context - Services
        builder.Services.AddScoped<IVehicleCommandService, VehicleCommandService>();
        builder.Services.AddScoped<IVehicleQueryService, VehicleQueryService>();
    }
}