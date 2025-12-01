namespace Rutana.API.Fleet.Interfaces.REST.Resources;

/// <summary>
/// Resource to update a vehicle's state.
/// </summary>
/// <param name="State">The new state (enabled/disabled).</param>
public record UpdateVehicleStateResource(string State);