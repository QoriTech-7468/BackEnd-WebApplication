using Rutana.API.Shared.Domain.Repositories;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Rutana.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddSharedContextServices(this WebApplicationBuilder builder)
    {
        // Profiles Bounded Context Dependency Injection Configuration
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}