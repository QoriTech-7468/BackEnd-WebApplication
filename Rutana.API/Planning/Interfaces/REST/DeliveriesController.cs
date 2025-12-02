using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.Planning.Domain.Model.Queries;
using Rutana.API.Planning.Domain.Services;
using Rutana.API.Planning.Interfaces.REST.Resources;
using Rutana.API.Planning.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Rutana.API.Planning.Interfaces.REST;

/// <summary>
/// Deliveries controller for managing deliveries and available locations.
/// </summary>
/// <param name="routeQueryService">The route query service.</param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Delivery Endpoints")]
public class DeliveriesController(
    IRouteQueryService routeQueryService) : ControllerBase
{
    /// <summary>
    /// Get available locations for creating deliveries on a specific execution date.
    /// </summary>
    /// <param name="executionDate">The execution date to filter by.</param>
    /// <returns>A list of available location resources.</returns>
    [HttpGet("available-locations")]
    [SwaggerOperation(
        Summary = "Get available locations for deliveries",
        Description = "Get all locations that are available (not used) for creating deliveries on a specific execution date for the current user's organization",
        OperationId = "GetAvailableLocationsForDeliveries")]
    [SwaggerResponse(StatusCodes.Status200OK, "The available locations were found", typeof(IEnumerable<AvailableLocationResource>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User not authenticated or not associated with an organization")]
    public async Task<IActionResult> GetAvailableLocationsForDeliveries([FromQuery] DateTime executionDate)
    {
        // Get authenticated user from HttpContext.Items (set by RequestAuthorizationMiddleware)
        var user = HttpContext.Items["User"] as User;
        
        if (user == null || user.OrganizationId == null)
        {
            return Unauthorized("User not authenticated or not associated with an organization");
        }
        
        var organizationId = user.OrganizationId.Value;
        var query = new GetAvailableLocationsForDeliveriesQuery(organizationId, executionDate);
        var availableLocations = await routeQueryService.Handle(query);
        var resources = availableLocations.Select(AvailableLocationResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}

