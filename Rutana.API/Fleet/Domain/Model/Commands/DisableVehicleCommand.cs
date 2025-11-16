namespace Rutana.API.Fleet.Domain.Model.Commands;

/// <summary>
/// Command to disable a vehicle.
/// </summary>
/// <param name="VehicleId"></param>
public record DisableVehicleCommand(
    int VehicleId);