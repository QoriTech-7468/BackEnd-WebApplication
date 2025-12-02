using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.IAM.Domain.Model.Enums;
using Rutana.API.IAM.Domain.Model.Queries;
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
[SwaggerTag("Available User endpoints")]
public class UsersController: ControllerBase
{
    private readonly IUserQueryService userQueryService;
    private readonly IUserCommandService userCommandService;
    
    public UsersController(
        IUserQueryService userQueryService,
        IUserCommandService userCommandService)
    {
        this.userQueryService = userQueryService;
        this.userCommandService = userCommandService;
    }

    private User? GetAuthenticatedUser()
    {
        return HttpContext.Items["User"] as User;
    }
    
    [AllowAnonymous]
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get user by id", Description = "Get a user by its identifier", OperationId = "GetUserById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was found", typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The user was not found")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        var user = await userQueryService.Handle(getUserByIdQuery);
        if (user == null) return NotFound();
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(userResource);
    }
    [AllowAnonymous]
    [HttpGet]
    [SwaggerOperation(Summary = "Get all users", Description = "Get all users", OperationId = "GetAllUsers")]
    [SwaggerResponse(StatusCodes.Status200OK, "The users were found", typeof(IEnumerable<UserResource>))]
    public async Task<IActionResult> GetAllUsers()
    {
        var getAllUsersQuery = new GetAllUsersQuery();
        var users = await userQueryService.Handle(getAllUsersQuery);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }
    [AllowAnonymous]
    [HttpPost] 
    [SwaggerOperation(Summary = "Create a new user", Description = "Create a new user", OperationId = "CreateUser")]
    [SwaggerResponse(StatusCodes.Status201Created, "The user was created", typeof(UserResource))]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserResource resource)
    {
        var command = CreateUserCommandFromResourceAssembler.ToCommandFromResource(resource);

        var user = await userCommandService.Handle(command);

        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);

        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, userResource);
    }

    [HttpGet("by-email")]
    [SwaggerOperation(Summary = "Get user by email", Description = "Get a user by email address", OperationId = "GetUserByEmail")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was found", typeof(UserByEmailResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The user was not found")]
    public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
    {
        var getUserByEmailQuery = new GetUserByEmailQuery(email);
        var user = await userQueryService.Handle(getUserByEmailQuery);
        if (user == null) return NotFound();
        var userResource = UserByEmailResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(userResource);
    }

    [HttpGet("organization")]
    [SwaggerOperation(Summary = "Get users by organization", Description = "Get all users from the authenticated user's organization", OperationId = "GetUsersByOrganization")]
    [SwaggerResponse(StatusCodes.Status200OK, "The users were found", typeof(IEnumerable<UserResource>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User not authenticated or has no organization")]
    public async Task<IActionResult> GetUsersByOrganization()
    {
        var authenticatedUser = GetAuthenticatedUser();
        if (authenticatedUser == null || authenticatedUser.OrganizationId == null)
        {
            return Unauthorized();
        }

        var query = new GetUsersByOrganizationIdQuery(authenticatedUser.OrganizationId.Value);
        var users = await userQueryService.Handle(query);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }

    [HttpPut("{id}/role")]
    [SwaggerOperation(Summary = "Change user role", Description = "Change a user's role (Admin or Owner only)", OperationId = "ChangeUserRole")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user role was changed", typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Only Admin or Owner can change user roles")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> ChangeUserRole(int id, [FromBody] ChangeUserRoleResource resource)
    {
        var authenticatedUser = GetAuthenticatedUser();
        if (authenticatedUser == null || authenticatedUser.OrganizationId == null)
        {
            return Unauthorized();
        }

        // Validar que el usuario tenga role Admin u Owner
        if (authenticatedUser.Role != UserRole.Admin && authenticatedUser.Role != UserRole.Owner)
        {
            return Forbid("Only Admin or Owner can change user roles");
        }

        try
        {
            // Obtener el usuario a modificar
            var getUserByIdQuery = new GetUserByIdQuery(id);
            var userToModify = await userQueryService.Handle(getUserByIdQuery);
            if (userToModify == null)
            {
                return NotFound();
            }

            // Validar que el usuario pertenece a la misma organización
            if (userToModify.OrganizationId == null || userToModify.OrganizationId.Value != authenticatedUser.OrganizationId.Value)
            {
                return Forbid("You can only change roles of users from your own organization");
            }

            // Validar que el usuario no sea Owner
            if (userToModify.Role == UserRole.Owner)
            {
                return Forbid("Cannot change the role of an Owner");
            }

            var command = ChangeUserRoleCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var updatedUser = await userCommandService.Handle(command);
            var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(updatedUser);
            
            return Ok(userResource);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpDelete("{id}/organization")]
    [SwaggerOperation(Summary = "Remove user from organization", Description = "Remove a user from the organization (Admin or Owner only)", OperationId = "RemoveUserFromOrganization")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was removed from the organization", typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Only Admin or Owner can remove users from organization")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> RemoveUserFromOrganization(int id)
    {
        var authenticatedUser = GetAuthenticatedUser();
        if (authenticatedUser == null || authenticatedUser.OrganizationId == null)
        {
            return Unauthorized();
        }

        // Validar que el usuario tenga role Admin u Owner
        if (authenticatedUser.Role != UserRole.Admin && authenticatedUser.Role != UserRole.Owner)
        {
            return Forbid("Only Admin or Owner can remove users from organization");
        }

        try
        {
            // Obtener el usuario a eliminar
            var getUserByIdQuery = new GetUserByIdQuery(id);
            var userToRemove = await userQueryService.Handle(getUserByIdQuery);
            if (userToRemove == null)
            {
                return NotFound();
            }

            // Validar que el usuario pertenece a la misma organización
            if (userToRemove.OrganizationId == null || userToRemove.OrganizationId.Value != authenticatedUser.OrganizationId.Value)
            {
                return Forbid("You can only remove users from your own organization");
            }

            // Validar que el usuario no sea Owner
            if (userToRemove.Role == UserRole.Owner)
            {
                return Forbid("Cannot remove an Owner from the organization");
            }

            var command = new Domain.Model.Commands.RemoveUserFromOrganizationCommand(id);
            var updatedUser = await userCommandService.Handle(command);
            var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(updatedUser);
            
            return Ok(userResource);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}