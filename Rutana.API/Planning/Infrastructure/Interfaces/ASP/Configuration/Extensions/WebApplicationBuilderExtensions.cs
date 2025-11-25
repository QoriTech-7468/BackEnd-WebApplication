using Rutana.API.Planning.Application.Internal.CommandServices;
using Rutana.API.Planning.Application.Internal.QueryServices;
using Rutana.API.Planning.Domain.Repositories;
using Rutana.API.Planning.Domain.Services;
using Rutana.API.Planning.Infrastructure.Persistence.EFC.Repositories;

namespace Rutana.API.Planning.Infrastructure.Interfaces.ASP.Configuration.Extensions;

/// <summary>
/// Web application builder extensions for Planning bounded context.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Adds Planning context services to the application.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    public static void AddPlanningContextServices(this WebApplicationBuilder builder)
    {
        // Planning Bounded Context - Repositories
        builder.Services.AddScoped<IRouteDraftRepository, RouteDraftRepository>();
        builder.Services.AddScoped<IRouteRepository, RouteRepository>();

        // Planning Bounded Context - Services
        builder.Services.AddScoped<IRouteCommandService, RouteCommandService>();
        builder.Services.AddScoped<IRouteQueryService, RouteQueryService>();
    }
}