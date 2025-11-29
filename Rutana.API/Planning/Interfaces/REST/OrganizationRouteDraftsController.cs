using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Rutana.API.Planning.Domain.Model.Queries;
using Rutana.API.Planning.Domain.Services;
using Rutana.API.Planning.Interfaces.REST.Resources;
using Rutana.API.Planning.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Rutana.API.Planning.Interfaces.REST;

/// <summary>
/// Organization route drafts controller for managing route drafts by organization.
/// </summary>
/// <param name="routeQueryService">The route query service.</param>
[ApiController]
[Route("api/v1/organizations/{organizationId:int}/route-drafts")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Organizations")]
public class OrganizationRouteDraftsController(
    IRouteQueryService routeQueryService) : ControllerBase
{
    /// <summary>
    /// Get all route drafts by organization id.
    /// </summary>
    /// <param name="organizationId">The organization identifier.</param>
    /// <returns>The list of route drafts.</returns>
    [HttpGet]
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
}