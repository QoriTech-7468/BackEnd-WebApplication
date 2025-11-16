namespace Rutana.API.Fleet.Interfaces.REST.Resources;

/// <summary>
/// Resource for updating a vehicle's profile.
/// </summary>
/// <param name="Plate">The new license plate.</param>
/// <param name="CapacityKg">The new capacity in kilograms.</param>
public record UpdateVehicleProfileResource(
    string Plate,
    decimal CapacityKg);