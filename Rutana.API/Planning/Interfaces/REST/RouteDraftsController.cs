using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Rutana.API.Planning.Domain.Model.Commands;
using Rutana.API.Planning.Domain.Model.Queries;
using Rutana.API.Planning.Domain.Services;
using Rutana.API.Planning.Interfaces.REST.Resources;
using Rutana.API.Planning.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Rutana.API.Planning.Interfaces.REST;

/// <summary>
/// Route drafts controller for managing route drafts.
/// </summary>
/// <param name="routeCommandService">The route command service.</param>
/// <param name="routeQueryService">The route query service.</param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Route Draft Endpoints")]
public class RouteDraftsController(
    IRouteCommandService routeCommandService,
    IRouteQueryService routeQueryService) : ControllerBase
{
    /// <summary>
    /// Get route draft by id.
    /// </summary>
    /// <param name="routeDraftId">The route draft identifier.</param>
    /// <returns>The route draft resource.</returns>
    [HttpGet("{routeDraftId:int}")]
    [SwaggerOperation(
        Summary = "Get route draft by id",
        Description = "Get a route draft by its unique identifier",
        OperationId = "GetRouteDraftById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The route draft was found", typeof(RouteDraftResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The route draft was not found")]
    public async Task<IActionResult> GetRouteDraftById(int routeDraftId)
    {
        var getRouteDraftByIdQuery = new GetRouteDraftByIdQuery(routeDraftId);
        var routeDraft = await routeQueryService.Handle(getRouteDraftByIdQuery);
        if (routeDraft is null) return NotFound();
        var resource = RouteDraftResourceFromEntityAssembler.ToResourceFromEntity(routeDraft);
        return Ok(resource);
    }

    /// <summary>
    /// Get all route drafts by organization id.
    /// </summary>
    /// <param name="organizationId">The organization identifier.</param>
    /// <returns>The list of route drafts.</returns>
    [HttpGet("organization/{organizationId:int}")]
    [SwaggerOperation(
        Summary = "Get route drafts by organization",
        Description = "Get all route drafts belonging to an organization",
        OperationId = "GetRouteDraftsByOrganizationId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of route drafts", typeof(IEnumerable<RouteDraftResource>))]
    public async Task<IActionResult> GetRouteDraftsByOrganizationId(int organizationId)
    {
        var getRouteDraftsByOrganizationIdQuery = new GetRouteDraftsByOrganizationIdQuery(organizationId);
        var routeDrafts = await routeQueryService.Handle(getRouteDraftsByOrganizationIdQuery);
        var resources = routeDrafts.Select(RouteDraftResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    /// Create a new route draft.
    /// </summary>
    /// <param name="resource">The create route draft resource.</param>
    /// <returns>The created route draft resource.</returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new route draft",
        Description = "Create a new editable route draft",
        OperationId = "CreateRouteDraft")]
    [SwaggerResponse(StatusCodes.Status201Created, "The route draft was created", typeof(RouteDraftResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The route draft could not be created")]
    public async Task<IActionResult> CreateRouteDraft([FromBody] CreateRouteDraftResource resource)
    {
        var createRouteDraftCommand = CreateRouteDraftCommandFromResourceAssembler.ToCommandFromResource(resource);
        var routeDraft = await routeCommandService.Handle(createRouteDraftCommand);
        if (routeDraft is null) return BadRequest();
        var routeDraftResource = RouteDraftResourceFromEntityAssembler.ToResourceFromEntity(routeDraft);
        return CreatedAtAction(nameof(GetRouteDraftById), new { routeDraftId = routeDraft.Id }, routeDraftResource);
    }

    /// <summary>
    /// Save changes to a route draft.
    /// </summary>
    /// <param name="routeDraftId">The route draft identifier.</param>
    /// <param name="resource">The save route draft changes resource.</param>
    /// <returns>The updated route draft resource.</returns>
    [HttpPut("{routeDraftId:int}")]
    [SwaggerOperation(
        Summary = "Save route draft changes",
        Description = "Save changes to an existing route draft",
        OperationId = "SaveRouteDraftChanges")]
    [SwaggerResponse(StatusCodes.Status200OK, "The route draft was updated", typeof(RouteDraftResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The route draft was not found")]
    public async Task<IActionResult> SaveRouteDraftChanges(int routeDraftId, [FromBody] SaveRouteDraftChangesResource resource)
    {
        var saveRouteDraftChangesCommand = SaveRouteDraftChangesCommandFromResourceAssembler.ToCommandFromResource(routeDraftId, resource);
        var routeDraft = await routeCommandService.Handle(saveRouteDraftChangesCommand);
        if (routeDraft is null) return NotFound();
        var routeDraftResource = RouteDraftResourceFromEntityAssembler.ToResourceFromEntity(routeDraft);
        return Ok(routeDraftResource);
    }

    /// <summary>
    /// Publish a route draft as an active route.
    /// </summary>
    /// <param name="routeDraftId">The route draft identifier.</param>
    /// <returns>The published route resource.</returns>
    [HttpPost("{routeDraftId:int}/publish")]
    [SwaggerOperation(
        Summary = "Publish route draft",
        Description = "Publish a route draft as an active route",
        OperationId = "PublishRoute")]
    [SwaggerResponse(StatusCodes.Status201Created, "The route was published", typeof(RouteResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The route draft could not be published")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The route draft was not found")]
    public async Task<IActionResult> PublishRoute(int routeDraftId)
    {
        var publishRouteCommand = new PublishRouteCommand(routeDraftId);
        var route = await routeCommandService.Handle(publishRouteCommand);
        if (route is null) return BadRequest();
        var routeResource = RouteResourceFromEntityAssembler.ToResourceFromEntity(route);
        return CreatedAtAction(
            nameof(RoutesController.GetRouteById), 
            "Routes",
            new { routeId = route.Id }, 
            routeResource);
    }

    /// <summary>
    /// Delete a route draft.
    /// </summary>
    /// <param name="routeDraftId">The route draft identifier.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{routeDraftId:int}")]
    [SwaggerOperation(
        Summary = "Delete route draft",
        Description = "Delete a route draft",
        OperationId = "DeleteRouteDraft")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The route draft was deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The route draft was not found")]
    public async Task<IActionResult> DeleteRouteDraft(int routeDraftId)
    {
        var deleteRouteDraftCommand = new DeleteRouteDraftCommand(routeDraftId);
        var result = await routeCommandService.Handle(deleteRouteDraftCommand);
        if (!result) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Add a location to a route draft.
    /// </summary>
    /// <param name="routeDraftId">The route draft identifier.</param>
    /// <param name="resource">The add location resource.</param>
    /// <returns>The updated route draft resource.</returns>
    [HttpPost("{routeDraftId:int}/locations")]
    [SwaggerOperation(
        Summary = "Add location to route draft",
        Description = "Add a location to a route draft's delivery list",
        OperationId = "AddLocationToRoute")]
    [SwaggerResponse(StatusCodes.Status200OK, "The location was added", typeof(RouteDraftResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The location could not be added")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The route draft was not found")]
    public async Task<IActionResult> AddLocationToRoute(int routeDraftId, [FromBody] AddLocationToRouteResource resource)
    {
        var addLocationToRouteCommand = new AddLocationToRouteCommand(routeDraftId, resource.LocationId);
        var routeDraft = await routeCommandService.Handle(addLocationToRouteCommand);
        if (routeDraft is null) return BadRequest();
        var routeDraftResource = RouteDraftResourceFromEntityAssembler.ToResourceFromEntity(routeDraft);
        return Ok(routeDraftResource);
    }

    /// <summary>
    /// Assign a team member to a route draft.
    /// </summary>
    /// <param name="routeDraftId">The route draft identifier.</param>
    /// <param name="resource">The assign member resource.</param>
    /// <returns>The updated route draft resource.</returns>
    [HttpPost("{routeDraftId:int}/team-members")]
    [SwaggerOperation(
        Summary = "Assign team member to route draft",
        Description = "Assign a team member to a route draft",
        OperationId = "AssignMemberToVehicleTeam")]
    [SwaggerResponse(StatusCodes.Status200OK, "The team member was assigned", typeof(RouteDraftResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The team member could not be assigned")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The route draft was not found")]
    public async Task<IActionResult> AssignMemberToVehicleTeam(int routeDraftId, [FromBody] AssignMemberToVehicleTeamResource resource)
    {
        var assignMemberCommand = new AssignMemberToVehicleTeamCommand(routeDraftId, resource.UserId);
        var routeDraft = await routeCommandService.Handle(assignMemberCommand);
        if (routeDraft is null) return BadRequest();
        var routeDraftResource = RouteDraftResourceFromEntityAssembler.ToResourceFromEntity(routeDraft);
        return Ok(routeDraftResource);
    }

    /// <summary>
    /// Assign a vehicle to a route draft.
    /// </summary>
    /// <param name="routeDraftId">The route draft identifier.</param>
    /// <param name="resource">The assign vehicle resource.</param>
    /// <returns>The updated route draft resource.</returns>
    [HttpPatch("{routeDraftId:int}/vehicle")]
    [SwaggerOperation(
        Summary = "Assign vehicle to route draft",
        Description = "Assign a vehicle to a route draft",
        OperationId = "AssignVehicleToRoute")]
    [SwaggerResponse(StatusCodes.Status200OK, "The vehicle was assigned", typeof(RouteDraftResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The route draft was not found")]
    public async Task<IActionResult> AssignVehicleToRoute(int routeDraftId, [FromBody] AssignVehicleToRouteResource resource)
    {
        var assignVehicleCommand = new AssignVehicleToRouteCommand(routeDraftId, resource.VehicleId);
        var routeDraft = await routeCommandService.Handle(assignVehicleCommand);
        if (routeDraft is null) return NotFound();
        var routeDraftResource = RouteDraftResourceFromEntityAssembler.ToResourceFromEntity(routeDraft);
        return Ok(routeDraftResource);
    }
}