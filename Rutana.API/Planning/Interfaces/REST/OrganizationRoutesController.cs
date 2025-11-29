using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Rutana.API.Planning.Domain.Model.Queries;
using Rutana.API.Planning.Domain.Services;
using Rutana.API.Planning.Interfaces.REST.Resources;
using Rutana.API.Planning.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Rutana.API.Planning.Interfaces.REST;

/// <summary>
/// Organization routes controller for managing routes by organization.
/// </summary>
/// <param name="routeQueryService">The route query service.</param>
[ApiController]
[Route("api/v1/organizations/{organizationId:int}/routes")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Organizations")]
public class OrganizationRoutesController(
    IRouteQueryService routeQueryService) : ControllerBase
{
    /// <summary>
    /// Get all routes by organization id.
    /// </summary>
    /// <param name="organizationId">The organization identifier.</param>
    /// <returns>The list of routes.</returns>
    [HttpGet]
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