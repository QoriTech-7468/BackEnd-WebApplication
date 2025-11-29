using Rutana.API.CRM.Application.ACL.Services;
using Rutana.API.CRM.Application.Internal.CommandServices;
using Rutana.API.CRM.Application.Internal.QueryServices;
using Rutana.API.CRM.Domain.Repositories;
using Rutana.API.CRM.Domain.Services;
using Rutana.API.CRM.Infrastructure.Persistence.EFC.Repositories;
using Rutana.API.CRM.Interfaces.ACL;

namespace Rutana.API.CRM.Infrastructure.Interfaces.ASP.Configuration.Extensions;

/// <summary>
/// Web application builder extensions for CRM bounded context.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Adds CRM context services to the application.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    public static void AddCRMContextServices(this WebApplicationBuilder builder)
    {
        // CRM Bounded Context - Repositories
        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<ILocationRepository, LocationRepository>();

        // CRM Bounded Context - Services
        builder.Services.AddScoped<IClientCommandService, ClientCommandService>();
        builder.Services.AddScoped<IClientQueryService, ClientQueryService>();
        builder.Services.AddScoped<ILocationCommandService, LocationCommandService>();
        builder.Services.AddScoped<ILocationQueryService, LocationQueryService>();

        // CRM Bounded Context - ACL (Anti-Corruption Layer)
        builder.Services.AddScoped<ICrmContextFacade, CrmContextFacade>();
    }
}