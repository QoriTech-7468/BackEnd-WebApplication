using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Rutana.API.Fleet.Domain.Model.Commands;
using Rutana.API.Fleet.Domain.Model.Queries;
using Rutana.API.Fleet.Domain.Services;
using Rutana.API.Fleet.Interfaces.REST.Resources;
using Rutana.API.Fleet.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Rutana.API.Fleet.Interfaces.REST;

/// <summary>
/// Vehicles controller for managing fleet vehicles.
/// </summary>
/// <param name="vehicleCommandService">The vehicle command service.</param>
/// <param name="vehicleQueryService">The vehicle query service.</param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Vehicle Endpoints")]
public class VehiclesController(
    IVehicleCommandService vehicleCommandService,
    IVehicleQueryService vehicleQueryService) : ControllerBase
{
    /// <summary>
    /// Get vehicle by id.
    /// </summary>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <returns>The vehicle resource.</returns>
    [HttpGet("{vehicleId:int}")]
    [SwaggerOperation(
        Summary = "Get vehicle by id",
        Description = "Get a vehicle by its unique identifier",
        OperationId = "GetVehicleById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The vehicle was found", typeof(VehicleResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The vehicle was not found")]
    public async Task<IActionResult> GetVehicleById(int vehicleId)
    {
        var getVehicleByIdQuery = new GetVehicleByIdQuery(vehicleId);
        var vehicle = await vehicleQueryService.Handle(getVehicleByIdQuery);
        if (vehicle is null) return NotFound();
        var resource = VehicleResourceFromEntityAssembler.ToResourceFromEntity(vehicle);
        return Ok(resource);
    }

    /// <summary>
    /// Register a new vehicle.
    /// </summary>
    /// <param name="resource">The register vehicle resource.</param>
    /// <returns>The created vehicle resource.</returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Register a new vehicle",
        Description = "Register a new vehicle in the fleet",
        OperationId = "RegisterVehicle")]
    [SwaggerResponse(StatusCodes.Status201Created, "The vehicle was created", typeof(VehicleResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The vehicle could not be created")]
    public async Task<IActionResult> RegisterVehicle([FromBody] RegisterVehicleResource resource)
    {
        var registerVehicleCommand = RegisterVehicleCommandFromResourceAssembler.ToCommandFromResource(resource);
        var vehicle = await vehicleCommandService.Handle(registerVehicleCommand);
        if (vehicle is null) return BadRequest();
        var vehicleResource = VehicleResourceFromEntityAssembler.ToResourceFromEntity(vehicle);
        return CreatedAtAction(nameof(GetVehicleById), new { vehicleId = vehicle.Id }, vehicleResource);
    }

    /// <summary>
    /// Update a vehicle's profile.
    /// </summary>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <param name="resource">The update vehicle profile resource.</param>
    /// <returns>The updated vehicle resource.</returns>
    [HttpPut("{vehicleId:int}")]
    [SwaggerOperation(
        Summary = "Update vehicle profile",
        Description = "Update a vehicle's profile information",
        OperationId = "UpdateVehicleProfile")]
    [SwaggerResponse(StatusCodes.Status200OK, "The vehicle was updated", typeof(VehicleResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The vehicle was not found")]
    public async Task<IActionResult> UpdateVehicleProfile(int vehicleId, [FromBody] UpdateVehicleProfileResource resource)
    {
        var updateVehicleProfileCommand = UpdateVehicleProfileCommandFromResourceAssembler.ToCommandFromResource(vehicleId, resource);
        var vehicle = await vehicleCommandService.Handle(updateVehicleProfileCommand);
        if (vehicle is null) return NotFound();
        var vehicleResource = VehicleResourceFromEntityAssembler.ToResourceFromEntity(vehicle);
        return Ok(vehicleResource);
    }

    /// <summary>
    /// Update a vehicle's state.
    /// </summary>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <param name="resource">The update vehicle state resource.</param>
    /// <returns>The updated vehicle resource.</returns>
    [HttpPatch("{vehicleId:int}/state")]
    [SwaggerOperation(
        Summary = "Update vehicle state",
        Description = "Update a vehicle's state (enabled/disabled)",
        OperationId = "UpdateVehicleState")]
    [SwaggerResponse(StatusCodes.Status200OK, "The vehicle state was updated", typeof(VehicleResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid state provided")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The vehicle was not found")]
    public async Task<IActionResult> UpdateVehicleState(int vehicleId, [FromBody] UpdateVehicleStateResource resource)
    {
        var updateVehicleStateCommand = UpdateVehicleStateCommandFromResourceAssembler.ToCommandFromResource(vehicleId, resource);
        var vehicle = await vehicleCommandService.Handle(updateVehicleStateCommand);
        if (vehicle is null) return BadRequest();
        var vehicleResource = VehicleResourceFromEntityAssembler.ToResourceFromEntity(vehicle);
        return Ok(vehicleResource);
    }
}