using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Rutana.API.CRM.Domain.Model.Commands;
using Rutana.API.CRM.Domain.Model.Queries;
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
    /// Get client by id.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <returns>The client resource.</returns>
    [HttpGet("{clientId:int}")]
    [SwaggerOperation(
        Summary = "Get client by id",
        Description = "Get a client by its unique identifier",
        OperationId = "GetClientById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The client was found", typeof(ClientResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The client was not found")]
    public async Task<IActionResult> GetClientById(int clientId)
    {
        var getClientByIdQuery = new GetClientByIdQuery(clientId);
        var client = await clientQueryService.Handle(getClientByIdQuery);
        if (client is null) return NotFound();
        var resource = ClientResourceFromEntityAssembler.ToResourceFromEntity(client);
        return Ok(resource);
    }

    /// <summary>
    /// Get client with its locations by id.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <returns>The client with locations resource.</returns>
    [HttpGet("{clientId:int}/with-locations")]
    [SwaggerOperation(
        Summary = "Get client with locations",
        Description = "Get a client with all its locations by its unique identifier",
        OperationId = "GetClientWithLocations")]
    [SwaggerResponse(StatusCodes.Status200OK, "The client with locations was found", typeof(ClientWithLocationsResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The client was not found")]
    public async Task<IActionResult> GetClientWithLocations(int clientId)
    {
        var getClientByIdQuery = new GetClientByIdQuery(clientId);
        var client = await clientQueryService.Handle(getClientByIdQuery);
        if (client is null) return NotFound();

        var getLocationsByClientIdQuery = new GetLocationsByClientIdQuery(clientId);
        var locations = await locationQueryService.Handle(getLocationsByClientIdQuery);

        var resource = ClientWithLocationsResourceAssembler.ToResourceFromEntity(client, locations);
        return Ok(resource);
    }

    /// <summary>
    /// Get all clients by organization id.
    /// </summary>
    /// <param name="organizationId">The organization identifier.</param>
    /// <returns>The list of clients.</returns>
    [HttpGet("organization/{organizationId:int}")]
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
        return CreatedAtAction(nameof(GetClientById), new { clientId = client.Id }, clientResource);
    }

    /// <summary>
    /// Enable a client.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <returns>The updated client resource.</returns>
    [HttpPatch("{clientId:int}/enable")]
    [SwaggerOperation(
        Summary = "Enable client",
        Description = "Enable a client",
        OperationId = "EnableClient")]
    [SwaggerResponse(StatusCodes.Status200OK, "The client was enabled", typeof(ClientResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The client was not found")]
    public async Task<IActionResult> EnableClient(int clientId)
    {
        var enableClientCommand = new EnableClientCommand(clientId);
        var client = await clientCommandService.Handle(enableClientCommand);
        if (client is null) return NotFound();
        var clientResource = ClientResourceFromEntityAssembler.ToResourceFromEntity(client);
        return Ok(clientResource);
    }

    /// <summary>
    /// Disable a client.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <returns>The updated client resource.</returns>
    [HttpPatch("{clientId:int}/disable")]
    [SwaggerOperation(
        Summary = "Disable client",
        Description = "Disable a client",
        OperationId = "DisableClient")]
    [SwaggerResponse(StatusCodes.Status200OK, "The client was disabled", typeof(ClientResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The client was not found")]
    public async Task<IActionResult> DisableClient(int clientId)
    {
        var disableClientCommand = new DisableClientCommand(clientId);
        var client = await clientCommandService.Handle(disableClientCommand);
        if (client is null) return NotFound();
        var clientResource = ClientResourceFromEntityAssembler.ToResourceFromEntity(client);
        return Ok(clientResource);
    }
}