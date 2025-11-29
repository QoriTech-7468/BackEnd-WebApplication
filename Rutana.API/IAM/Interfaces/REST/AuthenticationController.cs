using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Rutana.API.IAM.Domain.Services;
using Rutana.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using Rutana.API.IAM.Interfaces.REST.Resources;
using Rutana.API.IAM.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Rutana.API.IAM.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication endpoints")]
public class AuthenticationController(IUserCommandService userCommandService) : ControllerBase
{
    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Sign in", Description = "Sign in a user", OperationId = "SignIn")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was authenticated", typeof(AuthenticatedUserResource))]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
    {
        var command = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
        var authenticatedUser = await userCommandService.Handle(command);
        var resourceResponse = AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(authenticatedUser.user, authenticatedUser.token);
        return Ok(resourceResponse);
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Sign up", Description = "Sign up a new user", OperationId = "SignUp")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was created successfully")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
    {
        var command = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
        await userCommandService.Handle(command);
        return Ok(new { message = "User created successfully" });
    }
}