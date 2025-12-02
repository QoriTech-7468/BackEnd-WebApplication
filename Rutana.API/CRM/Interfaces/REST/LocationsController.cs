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
/// Locations controller for managing CRM locations.
/// </summary>
/// <param name="locationCommandService">The location command service.</param>
/// <param name="locationQueryService">The location query service.</param>
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
    /// Get all locations, optionally filtered by active status and client ID.
    /// </summary>
    /// <param name="isActive">Optional filter by active status.</param>
    /// <param name="clientId">Optional filter by client ID.</param>
    /// <returns>The list of locations.</returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all locations",
        Description = "Get all locations, optionally filtered by active status and client ID with ?isActive=true/false&clientId={id}",
        OperationId = "GetAllLocations")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of locations", typeof(IEnumerable<LocationResource>))]
    public async Task<IActionResult> GetAllLocations([FromQuery] bool? isActive = null, [FromQuery] int? clientId = null)
    {
        var getAllLocationsQuery = new GetAllLocationsQuery(isActive, clientId);
        var locations = await locationQueryService.Handle(getAllLocationsQuery);
        var resources = locations.Select(location => 
            LocationResourceFromEntityAssembler.ToResourceFromEntity(location));
        return Ok(resources);
    }

    /// <summary>
    /// Get location by id, optionally including client.
    /// </summary>
    /// <param name="locationId">The location identifier.</param>
    /// <param name="include">Optional parameter to include related data (e.g., 'client').</param>
    /// <returns>The location resource.</returns>
    [HttpGet("{locationId:int}")]
    [SwaggerOperation(
        Summary = "Get location by id",
        Description = "Get a location by its unique identifier, optionally including client with ?include=client",
        OperationId = "GetLocationById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The location was found", typeof(LocationResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The location was not found")]
    public async Task<IActionResult> GetLocationById(int locationId, [FromQuery] string? include = null)
    {
        var locationIdVo = new LocationId(locationId);
        var getLocationByIdQuery = new GetLocationByIdQuery(locationIdVo);
        var location = await locationQueryService.Handle(getLocationByIdQuery);
        if (location is null) return NotFound();
        
        // Note: Currently LocationResource always includes ClientId, so include=client doesn't change the response
        // This is kept for API consistency with the frontend expectations
        var resource = LocationResourceFromEntityAssembler.ToResourceFromEntity(location);
        return Ok(resource);
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
        
        //  Usar sobrecarga sin Client
        var locationResource = LocationResourceFromEntityAssembler.ToResourceFromEntity(location);
        return CreatedAtAction(nameof(GetLocationById), new { locationId = location.Id.Value }, locationResource);
    }

    /// <summary>
    /// Update a location.
    /// </summary>
    /// <param name="locationId">The location identifier.</param>
    /// <param name="resource">The update location resource.</param>
    /// <returns>The updated location resource.</returns>
    [HttpPut("{locationId:int}")]
    [SwaggerOperation(
        Summary = "Update a location",
        Description = "Update a location's information",
        OperationId = "UpdateLocation")]
    [SwaggerResponse(StatusCodes.Status200OK, "The location was updated", typeof(LocationResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The location could not be updated")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The location was not found")]
    public async Task<IActionResult> UpdateLocation(int locationId, [FromBody] UpdateLocationResource resource)
    {
        if (resource.Id != locationId)
            return BadRequest("Location ID in URL does not match the resource ID.");

        var updateLocationCommand = UpdateLocationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var location = await locationCommandService.Handle(updateLocationCommand);
        if (location is null) return NotFound();
        var locationResource = LocationResourceFromEntityAssembler.ToResourceFromEntity(location);
        return Ok(locationResource);
    }

    /// <summary>
    /// Update a location's status.
    /// </summary>
    /// <param name="locationId">The location identifier.</param>
    /// <param name="resource">The update location status resource.</param>
    /// <returns>The updated location resource.</returns>
    [HttpPatch("{locationId:int}/status")]
    [SwaggerOperation(
        Summary = "Update location status",
        Description = "Update a location's status (activate/deactivate)",
        OperationId = "UpdateLocationStatus")]
    [SwaggerResponse(StatusCodes.Status200OK, "The location status was updated", typeof(LocationResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid status provided")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The location was not found")]
    public async Task<IActionResult> UpdateLocationStatus(int locationId, [FromBody] UpdateLocationStatusResource resource)
    {
        var updateLocationStateCommand = UpdateLocationStatusCommandFromResourceAssembler.ToCommandFromResource(locationId, resource);
        var location = await locationCommandService.Handle(updateLocationStateCommand);
        if (location is null) return NotFound();
        var locationResource = LocationResourceFromEntityAssembler.ToResourceFromEntity(location);
        return Ok(locationResource);
    }
}