using Rutana.API.IAM.Application.Internal.OutboundServices;
using Rutana.API.IAM.Domain.Model.Queries;
using Rutana.API.IAM.Domain.Services;
using Rutana.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace Rutana.API.IAM.Infrastructure.Pipeline.Middleware.Components;

public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        Console.WriteLine("Entering InvokeAsync");
        
        // GetEndpoint() can be null for unmatched routes or Swagger UI
        var endpoint = context.GetEndpoint();
        var allowAnonymous = endpoint?.Metadata
            .Any(m => m.GetType() == typeof(AllowAnonymousAttribute)) ?? false;
        
        if (allowAnonymous)
        {
            Console.WriteLine("Skipping authorization");
            await next(context);
            return;
        }

        Console.WriteLine("Entering authorization");
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token == null) throw new Exception("Null or invalid token");

        var userId = await tokenService.ValidateToken(token);

        if (userId == null) throw new Exception("Invalid token");

        var getUserByIdQuery = new GetUserByIdQuery(userId.Value);
        var user = await userQueryService.Handle(getUserByIdQuery);
        
        Console.WriteLine("Successful authorization. Updating Context...");
        context.Items["User"] = user;
        
        await next(context);
    }
}