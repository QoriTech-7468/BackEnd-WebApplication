using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Rutana.API.Fleet.Domain.Model.Queries;
using Rutana.API.Fleet.Domain.Services;
using Rutana.API.Fleet.Interfaces.REST.Resources;
using Rutana.API.Fleet.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Rutana.API.Fleet.Interfaces.REST;

/// <summary>
/// Organization vehicles controller for managing vehicles by organization.
/// </summary>
/// <param name="vehicleQueryService">The vehicle query service.</param>
[ApiController]
[Route("api/v1/organizations/{organizationId:int}/vehicles")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Organizations")]
public class OrganizationVehiclesController(
    IVehicleQueryService vehicleQueryService) : ControllerBase
{
    /// <summary>
    /// Get all vehicles by organization id with optional state filter.
    /// </summary>
    /// <param name="organizationId">The organization identifier.</param>
    /// <param name="state">Optional state filter (enabled/disabled).</param>
    /// <returns>The list of vehicles.</returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get vehicles by organization",
        Description = "Get all vehicles belonging to an organization, optionally filtered by state",
        OperationId = "GetVehiclesByOrganizationId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of vehicles", typeof(IEnumerable<VehicleResource>))]
    public async Task<IActionResult> GetVehiclesByOrganizationId(
        int organizationId,
        [FromQuery] string? state = null)
    {
        IEnumerable<VehicleResource> resources;

        if (!string.IsNullOrWhiteSpace(state) && state.Equals("enabled", StringComparison.OrdinalIgnoreCase))
        {
            var getEnabledVehiclesQuery = new GetEnabledVehiclesByOrganizationIdQuery(organizationId);
            var enabledVehicles = await vehicleQueryService.Handle(getEnabledVehiclesQuery);
            resources = enabledVehicles.Select(VehicleResourceFromEntityAssembler.ToResourceFromEntity);
        }
        else
        {
            var getVehiclesQuery = new GetVehiclesByOrganizationIdQuery(organizationId);
            var vehicles = await vehicleQueryService.Handle(getVehiclesQuery);
            resources = vehicles.Select(VehicleResourceFromEntityAssembler.ToResourceFromEntity);
        }

        return Ok(resources);
    }
}