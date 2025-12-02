using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Rutana.API.CRM.Domain.Model.Queries;
using Rutana.API.CRM.Domain.Services;
using Rutana.API.CRM.Interfaces.REST.Resources;
using Rutana.API.CRM.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Rutana.API.CRM.Interfaces.REST;

/// <summary>
/// Organization clients controller for managing clients by organization.
/// </summary>
/// <param name="clientQueryService">The client query service.</param>
[ApiController]
[Route("api/v1/organizations/{organizationId:int}/clients")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Organizations")]
public class OrganizationClientsController(
    IClientQueryService clientQueryService) : ControllerBase
{
    /// <summary>
    /// Get all clients by organization id.
    /// </summary>
    /// <param name="organizationId">The organization identifier.</param>
    /// <returns>The list of clients.</returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get clients by organization",
        Description = "Get all clients belonging to an organization",
        OperationId = "GetClientsByOrganizationId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of clients", typeof(IEnumerable<ClientResource>))]
    public async Task<IActionResult> GetClientsByOrganizationId(int organizationId)
    {
        var getClientsByOrganizationIdQuery = new GetClientsByOrganizationIdQuery(organizationId);
        var clients = await clientQueryService.Handle(getClientsByOrganizationIdQuery);
        var resources = clients.Select(ClientResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}