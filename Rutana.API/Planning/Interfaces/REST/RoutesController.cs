using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
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

    /// <summary>
    /// Get all routes by organization id.
    /// </summary>
    /// <param name="organizationId">The organization identifier.</param>
    /// <returns>The list of routes.</returns>
    [HttpGet("organization/{organizationId:int}")]
    [SwaggerOperation(
        Summary = "Get routes by organization",
        Description = "Get all published routes belonging to an organization",
        OperationId = "GetRoutesByOrganizationId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of routes", typeof(IEnumerable<RouteResource>))]
    public async Task<IActionResult> GetRoutesByOrganizationId(int organizationId)
    {
        var getRoutesByOrganizationIdQuery = new GetRoutesByOrganizationIdQuery(organizationId);
        var routes = await routeQueryService.Handle(getRoutesByOrganizationIdQuery);
        var resources = routes.Select(RouteResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}