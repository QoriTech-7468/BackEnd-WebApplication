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
    /// Get all vehicles by organization id.
    /// </summary>
    /// <param name="organizationId">The organization identifier.</param>
    /// <returns>The list of vehicles.</returns>
    [HttpGet("organization/{organizationId:int}")]
    [SwaggerOperation(
        Summary = "Get vehicles by organization",
        Description = "Get all vehicles belonging to an organization",
        OperationId = "GetVehiclesByOrganizationId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of vehicles", typeof(IEnumerable<VehicleResource>))]
    public async Task<IActionResult> GetVehiclesByOrganizationId(int organizationId)
    {
        var getVehiclesByOrganizationIdQuery = new GetVehiclesByOrganizationIdQuery(organizationId);
        var vehicles = await vehicleQueryService.Handle(getVehiclesByOrganizationIdQuery);
        var resources = vehicles.Select(VehicleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    /// Get all enabled vehicles by organization id.
    /// </summary>
    /// <param name="organizationId">The organization identifier.</param>
    /// <returns>The list of enabled vehicles.</returns>
    [HttpGet("organization/{organizationId:int}/enabled")]
    [SwaggerOperation(
        Summary = "Get enabled vehicles by organization",
        Description = "Get all enabled vehicles belonging to an organization",
        OperationId = "GetEnabledVehiclesByOrganizationId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of enabled vehicles", typeof(IEnumerable<VehicleResource>))]
    public async Task<IActionResult> GetEnabledVehiclesByOrganizationId(int organizationId)
    {
        var getEnabledVehiclesByOrganizationIdQuery = new GetEnabledVehiclesByOrganizationIdQuery(organizationId);
        var vehicles = await vehicleQueryService.Handle(getEnabledVehiclesByOrganizationIdQuery);
        var resources = vehicles.Select(VehicleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
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
    /// Enable a vehicle.
    /// </summary>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <returns>The updated vehicle resource.</returns>
    [HttpPatch("{vehicleId:int}/enable")]
    [SwaggerOperation(
        Summary = "Enable vehicle",
        Description = "Enable a vehicle, making it available for routes",
        OperationId = "EnableVehicle")]
    [SwaggerResponse(StatusCodes.Status200OK, "The vehicle was enabled", typeof(VehicleResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The vehicle was not found")]
    public async Task<IActionResult> EnableVehicle(int vehicleId)
    {
        var enableVehicleCommand = new EnableVehicleCommand(vehicleId);
        var vehicle = await vehicleCommandService.Handle(enableVehicleCommand);
        if (vehicle is null) return NotFound();
        var vehicleResource = VehicleResourceFromEntityAssembler.ToResourceFromEntity(vehicle);
        return Ok(vehicleResource);
    }

    /// <summary>
    /// Disable a vehicle.
    /// </summary>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <returns>The updated vehicle resource.</returns>
    [HttpPatch("{vehicleId:int}/disable")]
    [SwaggerOperation(
        Summary = "Disable vehicle",
        Description = "Disable a vehicle, making it unavailable for routes",
        OperationId = "DisableVehicle")]
    [SwaggerResponse(StatusCodes.Status200OK, "The vehicle was disabled", typeof(VehicleResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The vehicle was not found")]
    public async Task<IActionResult> DisableVehicle(int vehicleId)
    {
        var disableVehicleCommand = new DisableVehicleCommand(vehicleId);
        var vehicle = await vehicleCommandService.Handle(disableVehicleCommand);
        if (vehicle is null) return NotFound();
        var vehicleResource = VehicleResourceFromEntityAssembler.ToResourceFromEntity(vehicle);
        return Ok(vehicleResource);
    }
}
