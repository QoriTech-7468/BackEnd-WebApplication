using Rutana.API.Suscriptions.Application.Internal.CommandServices;
using Rutana.API.Suscriptions.Application.Internal.QueryServices;
using Rutana.API.Suscriptions.Domain.Repositories;
using Rutana.API.Suscriptions.Domain.Services;
using Rutana.API.Suscriptions.Infrastructure.Persistence.EFC.Repositories;

namespace Rutana.API.Suscriptions.Infrastructure.Interfaces.ASP.Configuration.Extensions;

/// <summary>
/// Web application builder extensions for Subscriptions bounded context.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Adds Subscriptions context services to the application.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    public static void AddSubscriptionsContextServices(this WebApplicationBuilder builder)
    {
        // Subscriptions Bounded Context - Repositories
        builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();

        // Subscriptions Bounded Context - Services
        builder.Services.AddScoped<IOrganizationCommandService, OrganizationCommandService>();
        builder.Services.AddScoped<IOrganizationQueryService, OrganizationQueryService>();
    }
}

