using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Rutana.API.IAM.Domain.Model.Aggregates;
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
    /// Get route drafts by execution date.
    /// </summary>
    /// <param name="executionDate">The execution date to filter by.</param>
    /// <returns>A list of route draft summary resources.</returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get route drafts by execution date",
        Description = "Get all route drafts for a specific execution date for the current user's organization",
        OperationId = "GetRouteDraftsByExecutionDate")]
    [SwaggerResponse(StatusCodes.Status200OK, "The route drafts were found", typeof(IEnumerable<RouteDraftSummaryResource>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User not authenticated or not associated with an organization")]
    public async Task<IActionResult> GetRouteDraftsByExecutionDate([FromQuery] DateTime executionDate)
    {
        // Get authenticated user from HttpContext.Items (set by RequestAuthorizationMiddleware)
        var user = HttpContext.Items["User"] as User;
        
        if (user == null || user.OrganizationId == null)
        {
            return Unauthorized("User not authenticated or not associated with an organization");
        }
        
        var organizationId = user.OrganizationId.Value;
        var query = new GetRouteDraftsByExecutionDateQuery(organizationId, executionDate);
        var routeDrafts = await routeQueryService.Handle(query);
        var resources = routeDrafts.Select(RouteDraftSummaryResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

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
    /// Update route draft status (e.g., publish).
    /// </summary>
    /// <param name="routeDraftId">The route draft identifier.</param>
    /// <param name="resource">The update status resource.</param>
    /// <returns>The published route if status is "published".</returns>
    [HttpPatch("{routeDraftId:int}/status")]
    [SwaggerOperation(
        Summary = "Update route draft status",
        Description = "Update route draft status. Use 'published' to publish the route draft",
        OperationId = "UpdateRouteDraftStatus")]
    [SwaggerResponse(StatusCodes.Status200OK, "The status was updated", typeof(RouteResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid status provided")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The route draft was not found")]
    public async Task<IActionResult> UpdateRouteDraftStatus(int routeDraftId, [FromBody] UpdateRouteDraftStatusResource resource)
    {
        if (resource.Status.Equals("published", StringComparison.OrdinalIgnoreCase))
        {
            var publishRouteCommand = new PublishRouteCommand(routeDraftId);
            var route = await routeCommandService.Handle(publishRouteCommand);
            if (route is null) return BadRequest();
            var routeResource = RouteResourceFromEntityAssembler.ToResourceFromEntity(route);
            return Ok(routeResource);
        }

        return BadRequest("Invalid status. Valid values: 'published'");
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