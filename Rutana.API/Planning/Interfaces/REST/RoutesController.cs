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
/// Routes controller for managing published routes.
/// </summary>
/// <param name="routeQueryService">The route query service.</param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Route Endpoints")]
public class RoutesController(
    IRouteQueryService routeQueryService) : ControllerBase
{
    /// <summary>
    /// Get routes by execution date.
    /// </summary>
    /// <param name="executionDate">The execution date to filter by.</param>
    /// <returns>A list of route summary resources.</returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get routes by execution date",
        Description = "Get all routes for a specific execution date for the current user's organization",
        OperationId = "GetRoutesByExecutionDate")]
    [SwaggerResponse(StatusCodes.Status200OK, "The routes were found", typeof(IEnumerable<RouteSummaryResource>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User not authenticated or not associated with an organization")]
    public async Task<IActionResult> GetRoutesByExecutionDate([FromQuery] DateTime executionDate)
    {
        // Get authenticated user from HttpContext.Items (set by RequestAuthorizationMiddleware)
        var user = HttpContext.Items["User"] as User;
        
        if (user == null || user.OrganizationId == null)
        {
            return Unauthorized("User not authenticated or not associated with an organization");
        }
        
        var organizationId = user.OrganizationId.Value;
        var query = new GetRoutesByExecutionDateQuery(organizationId, executionDate);
        var routes = await routeQueryService.Handle(query);
        var resources = routes.Select(RouteSummaryResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    /// Get route by id.
    /// </summary>
    /// <param name="routeId">The route identifier.</param>
    /// <returns>The route resource.</returns>
    [HttpGet("{routeId:int}")]
    [SwaggerOperation(
        Summary = "Get route by id",
        Description = "Get a published route by its unique identifier",
        OperationId = "GetRouteById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The route was found", typeof(RouteResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The route was not found")]
    public async Task<IActionResult> GetRouteById(int routeId)
    {
        var getRouteByIdQuery = new GetRouteByIdQuery(routeId);
        var route = await routeQueryService.Handle(getRouteByIdQuery);
        if (route is null) return NotFound();
        var resource = RouteResourceFromEntityAssembler.ToResourceFromEntity(route);
        return Ok(resource);
    }
}