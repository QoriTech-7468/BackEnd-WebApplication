using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Rutana.API.CRM.Domain.Model.Queries;
using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.CRM.Domain.Services;
using Rutana.API.CRM.Interfaces.REST.Resources;
using Rutana.API.CRM.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Rutana.API.CRM.Interfaces.REST;

/// <summary>
/// Client locations controller for managing locations by client.
/// </summary>
/// <param name="locationQueryService">The location query service.</param>
[ApiController]
[Route("api/v1/clients/{clientId:int}/locations")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Clients")]
public class ClientLocationsController(
    ILocationQueryService locationQueryService) : ControllerBase
{
    /// <summary>
    /// Get all locations by client id.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <returns>The list of locations.</returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get locations by client",
        Description = "Get all locations belonging to a client",
        OperationId = "GetLocationsByClientId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of locations", typeof(IEnumerable<LocationResource>))]
    public async Task<IActionResult> GetLocationsByClientId(int clientId)
    {
        var clientIdVo = new ClientId(clientId);
        var getLocationsByClientIdQuery = new GetLocationsByClientIdQuery(clientIdVo);
        var locations = await locationQueryService.Handle(getLocationsByClientIdQuery);
        
        //  Usar la sobrecarga sin Client (solo con LocationId)
        var resources = locations.Select(location => 
            LocationResourceFromEntityAssembler.ToResourceFromEntity(location));
        
        return Ok(resources);
    }
}