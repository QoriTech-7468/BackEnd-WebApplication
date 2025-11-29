namespace Rutana.API.Planning.Domain.Model.Commands;

/// <summary>
/// Command to assign a vehicle to a route draft.
/// </summary>
/// <param name="RouteDraftId">The route draft identifier.</param>
/// <param name="VehicleId">The vehicle identifier to assign.</param>
public record AssignVehicleToRouteCommand(int RouteDraftId, int VehicleId);