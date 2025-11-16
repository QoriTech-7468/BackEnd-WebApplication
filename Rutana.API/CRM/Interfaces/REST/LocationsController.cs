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
/// Locations controller for managing client locations.
/// </summary>
/// <param name="locationCommandService">The location command service.</param>
/// <param name="locationQueryService">The location query service.</param>
/// <param name="clientQueryService">The client query service.</param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Location Endpoints")]
public class LocationsController(
    ILocationCommandService locationCommandService,
    ILocationQueryService locationQueryService,
    IClientQueryService clientQueryService) : ControllerBase
{
    /// <summary>
    /// Get location by id.
    /// </summary>
    /// <param name="locationId">The location identifier.</param>
    /// <returns>The location resource.</returns>
    [HttpGet("{locationId:int}")]
    [SwaggerOperation(
        Summary = "Get location by id",
        Description = "Get a location by its unique identifier",
        OperationId = "GetLocationById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The location was found", typeof(LocationResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The location was not found")]
    public async Task<IActionResult> GetLocationById(int locationId)
    {
        var getLocationByIdQuery = new GetLocationByIdQuery(locationId);
        var location = await locationQueryService.Handle(getLocationByIdQuery);
        if (location is null) return NotFound();

        // Get client info for the location
        var getClientByIdQuery = new GetClientByIdQuery(location.ClientId.Value);
        var client = await clientQueryService.Handle(getClientByIdQuery);
        if (client is null) return NotFound();

        var resource = LocationResourceFromEntityAssembler.ToResourceFromEntity(location, client);
        return Ok(resource);
    }

    /// <summary>
    /// Get all locations by client id.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <returns>The list of locations.</returns>
    [HttpGet("client/{clientId:int}")]
    [SwaggerOperation(
        Summary = "Get locations by client",
        Description = "Get all locations belonging to a client",
        OperationId = "GetLocationsByClientId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of locations", typeof(IEnumerable<LocationResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The client was not found")]
    public async Task<IActionResult> GetLocationsByClientId(int clientId)
    {
        // Verify client exists
        var getClientByIdQuery = new GetClientByIdQuery(clientId);
        var client = await clientQueryService.Handle(getClientByIdQuery);
        if (client is null) return NotFound();

        var getLocationsByClientIdQuery = new GetLocationsByClientIdQuery(clientId);
        var locations = await locationQueryService.Handle(getLocationsByClientIdQuery);
        var resources = locations.Select(location => 
            LocationResourceFromEntityAssembler.ToResourceFromEntity(location, client));
        return Ok(resources);
    }

    /// <summary>
    /// Register a new location.
    /// </summary>
    /// <param name="resource">The register location resource.</param>
    /// <returns>The created location resource.</returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Register a new location",
        Description = "Register a new location for a client",
        OperationId = "RegisterLocation")]
    [SwaggerResponse(StatusCodes.Status201Created, "The location was created", typeof(LocationResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The location could not be created")]
    public async Task<IActionResult> RegisterLocation([FromBody] RegisterLocationResource resource)
    {
        var registerLocationCommand = RegisterLocationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var location = await locationCommandService.Handle(registerLocationCommand);
        if (location is null) return BadRequest();

        // Get client info for the response
        var getClientByIdQuery = new GetClientByIdQuery(location.ClientId.Value);
        var client = await clientQueryService.Handle(getClientByIdQuery);
        if (client is null) return BadRequest();

        var locationResource = LocationResourceFromEntityAssembler.ToResourceFromEntity(location, client);
        return CreatedAtAction(nameof(GetLocationById), new { locationId = location.Id }, locationResource);
    }

    /// <summary>
    /// Enable a location.
    /// </summary>
    /// <param name="locationId">The location identifier.</param>
    /// <returns>The updated location resource.</returns>
    [HttpPatch("{locationId:int}/enable")]
    [SwaggerOperation(
        Summary = "Enable location",
        Description = "Enable a location",
        OperationId = "EnableLocation")]
    [SwaggerResponse(StatusCodes.Status200OK, "The location was enabled", typeof(LocationResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The location was not found")]
    public async Task<IActionResult> EnableLocation(int locationId)
    {
        var enableLocationCommand = new EnableLocationCommand(locationId);
        var location = await locationCommandService.Handle(enableLocationCommand);
        if (location is null) return NotFound();

        // Get client info for the response
        var getClientByIdQuery = new GetClientByIdQuery(location.ClientId.Value);
        var client = await clientQueryService.Handle(getClientByIdQuery);
        if (client is null) return NotFound();

        var locationResource = LocationResourceFromEntityAssembler.ToResourceFromEntity(location, client);
        return Ok(locationResource);
    }

    /// <summary>
    /// Disable a location.
    /// </summary>
    /// <param name="locationId">The location identifier.</param>
    /// <returns>The updated location resource.</returns>
    [HttpPatch("{locationId:int}/disable")]
    [SwaggerOperation(
        Summary = "Disable location",
        Description = "Disable a location",
        OperationId = "DisableLocation")]
    [SwaggerResponse(StatusCodes.Status200OK, "The location was disabled", typeof(LocationResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The location was not found")]
    public async Task<IActionResult> DisableLocation(int locationId)
    {
        var disableLocationCommand = new DisableLocationCommand(locationId);
        var location = await locationCommandService.Handle(disableLocationCommand);
        if (location is null) return NotFound();

        // Get client info for the response
        var getClientByIdQuery = new GetClientByIdQuery(location.ClientId.Value);
        var client = await clientQueryService.Handle(getClientByIdQuery);
        if (client is null) return NotFound();

        var locationResource = LocationResourceFromEntityAssembler.ToResourceFromEntity(location, client);
        return Ok(locationResource);
    }
}