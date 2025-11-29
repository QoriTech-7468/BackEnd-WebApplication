namespace Rutana.API.Fleet.Domain.Model.Commands;

/// <summary>
/// Command to update a vehicle's state.
/// </summary>
/// <param name="VehicleId">The vehicle identifier.</param>
/// <param name="State">The new state (enabled/disabled).</param>
public record UpdateVehicleStateCommand(int VehicleId, string State);