namespace Rutana.API.Planning.Interfaces.REST.Resources;

/// <summary>
/// Resource for assigning a vehicle to a route draft.
/// </summary>
/// <param name="VehicleId">The vehicle identifier to assign.</param>
public record AssignVehicleToRouteResource(int VehicleId);