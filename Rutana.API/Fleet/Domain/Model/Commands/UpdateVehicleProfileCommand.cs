namespace Rutana.API.Fleet.Domain.Model.Commands;

/// <summary>
/// Command to update a vehicle profile.
/// </summary>
/// <param name="VehicleId"></param>
/// <param name="Plate"></param>
/// <param name="CapacityKg"></param>
public record UpdateVehicleProfileCommand(
    int VehicleId,
    string Plate,
    decimal CapacityKg);