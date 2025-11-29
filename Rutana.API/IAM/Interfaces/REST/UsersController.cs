using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
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
}