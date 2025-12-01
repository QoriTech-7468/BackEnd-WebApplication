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
    ILocationQueryService locationQueryService) : ControllerBase
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
        var locationIdVo = new LocationId(locationId);
        var getLocationByIdQuery = new GetLocationByIdQuery(locationIdVo);
        var location = await locationQueryService.Handle(getLocationByIdQuery);
        if (location is null) return NotFound();
        
        //  Usar sobrecarga sin Client
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
    /// Update a location's state.
    /// </summary>
    /// <param name="locationId">The location identifier.</param>
    /// <param name="resource">The update location state resource.</param>
    /// <returns>The updated location resource.</returns>
    [HttpPatch("{locationId:int}/state")]
    [SwaggerOperation(
        Summary = "Update location state",
        Description = "Update a location's state (enabled/disabled)",
        OperationId = "UpdateLocationState")]
    [SwaggerResponse(StatusCodes.Status200OK, "The location state was updated", typeof(LocationResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid state provided")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The location was not found")]
    public async Task<IActionResult> UpdateLocationState(int locationId, [FromBody] UpdateLocationStateResource resource)
    {
        var updateLocationStateCommand = UpdateLocationStateCommandFromResourceAssembler.ToCommandFromResource(locationId, resource);
        var location = await locationCommandService.Handle(updateLocationStateCommand);
        if (location is null) return BadRequest();
        
        //  Usar sobrecarga sin Client
        var locationResource = LocationResourceFromEntityAssembler.ToResourceFromEntity(location);
        return Ok(locationResource);
    }
}