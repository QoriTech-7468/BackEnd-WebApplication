using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Rutana.API.CRM.Domain.Model.Commands;
using Rutana.API.CRM.Domain.Model.Queries;
using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.CRM.Domain.Services;
using Rutana.API.CRM.Interfaces.REST.Resources;
using Rutana.API.CRM.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Rutana.API.CRM.Interfaces.REST;

/// <summary>
/// Clients controller for managing CRM clients.
/// </summary>
/// <param name="clientCommandService">The client command service.</param>
/// <param name="clientQueryService">The client query service.</param>
/// <param name="locationQueryService">The location query service.</param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Client Endpoints")]
public class ClientsController(
    IClientCommandService clientCommandService,
    IClientQueryService clientQueryService,
    ILocationQueryService locationQueryService) : ControllerBase
{
    /// <summary>
    /// Get client by id, optionally including locations.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <param name="include">Optional parameter to include related data (e.g., 'locations').</param>
    /// <returns>The client resource.</returns>
    [HttpGet("{clientId:int}")]
    [SwaggerOperation(
        Summary = "Get client by id",
        Description = "Get a client by its unique identifier, optionally including locations with ?include=locations",
        OperationId = "GetClientById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The client was found", typeof(ClientResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The client was not found")]
    public async Task<IActionResult> GetClientById(int clientId, [FromQuery] string? include = null)
    {
        var clientIdVo = new ClientId(clientId);
        var getClientByIdQuery = new GetClientByIdQuery(clientIdVo);
        var client = await clientQueryService.Handle(getClientByIdQuery);
        if (client is null) return NotFound();

        // Check if locations should be included
        if (!string.IsNullOrWhiteSpace(include) && include.Equals("locations", StringComparison.OrdinalIgnoreCase))
        {
            var getLocationsByClientIdQuery = new GetLocationsByClientIdQuery(clientIdVo);
            var locations = await locationQueryService.Handle(getLocationsByClientIdQuery);
            var resourceWithLocations = ClientWithLocationsResourceAssembler.ToResourceFromEntity(client, locations);
            return Ok(resourceWithLocations);
        }

        var resource = ClientResourceFromEntityAssembler.ToResourceFromEntity(client);
        return Ok(resource);
    }

    /// <summary>
    /// Register a new client.
    /// </summary>
    /// <param name="resource">The register client resource.</param>
    /// <returns>The created client resource.</returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Register a new client",
        Description = "Register a new client in the CRM system",
        OperationId = "RegisterClient")]
    [SwaggerResponse(StatusCodes.Status201Created, "The client was created", typeof(ClientResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The client could not be created")]
    public async Task<IActionResult> RegisterClient([FromBody] RegisterClientResource resource)
    {
        var registerClientCommand = RegisterClientCommandFromResourceAssembler.ToCommandFromResource(resource);
        var client = await clientCommandService.Handle(registerClientCommand);
        if (client is null) return BadRequest();
        var clientResource = ClientResourceFromEntityAssembler.ToResourceFromEntity(client);
        return CreatedAtAction(nameof(GetClientById), new { clientId = client.Id.Value }, clientResource);
    }

    /// <summary>
    /// Update a client's state.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <param name="resource">The update client state resource.</param>
    /// <returns>The updated client resource.</returns>
    [HttpPatch("{clientId:int}/state")]
    [SwaggerOperation(
        Summary = "Update client state",
        Description = "Update a client's state (enabled/disabled)",
        OperationId = "UpdateClientState")]
    [SwaggerResponse(StatusCodes.Status200OK, "The client state was updated", typeof(ClientResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid state provided")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The client was not found")]
    public async Task<IActionResult> UpdateClientState(int clientId, [FromBody] UpdateClientStateResource resource)
    {
        var updateClientStateCommand = UpdateClientStateCommandFromResourceAssembler.ToCommandFromResource(clientId, resource);
        var client = await clientCommandService.Handle(updateClientStateCommand);
        if (client is null) return BadRequest();
        var clientResource = ClientResourceFromEntityAssembler.ToResourceFromEntity(client);
        return Ok(clientResource);
    }
}