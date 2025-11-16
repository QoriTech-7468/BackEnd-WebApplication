namespace Rutana.API.Fleet.Domain.Model.Commands;

/// <summary>
/// Command to enable a vehicle.
/// </summary>
/// <param name="VehicleId"></param>
public record EnableVehicleCommand(
    int VehicleId);