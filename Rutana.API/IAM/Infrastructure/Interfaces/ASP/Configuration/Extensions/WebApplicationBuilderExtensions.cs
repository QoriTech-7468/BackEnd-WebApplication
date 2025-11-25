using Rutana.API.IAM.Application.ACL.Services;
using Rutana.API.IAM.Application.Internal.CommandServices;
using Rutana.API.IAM.Application.Internal.OutboundServices;
using Rutana.API.IAM.Application.Internal.QueryServices;
using Rutana.API.IAM.Domain.Repositories;
using Rutana.API.IAM.Domain.Services;
using Rutana.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using Rutana.API.IAM.Infrastructure.Persistance.EFC.Repositories;
using Rutana.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using Rutana.API.IAM.Infrastructure.Tokens.JWT.Services;
using Rutana.API.IAM.Interfaces.ACL;

namespace Rutana.API.IAM.Interfaces.ASP.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddIamContextServices(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
        
        // Repositorios
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        // Servicios de Aplicación 
        builder.Services.AddScoped<IUserCommandService, UserCommandService>();
        builder.Services.AddScoped<IUserQueryService, UserQueryService>();

        // Servicios de Infraestructura 
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IHashingService, HashingService>();

        // ACL (Fachada para otros bounded contexts)
        builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();
    }
}