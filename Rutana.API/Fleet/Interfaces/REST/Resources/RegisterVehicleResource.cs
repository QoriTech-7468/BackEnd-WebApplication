namespace Rutana.API.Fleet.Interfaces.REST.Resources;

/// <summary>
/// Resource for registering a new vehicle.
/// </summary>
/// <param name="OrganizationId">The organization that will own the vehicle.</param>
/// <param name="Plate">The license plate of the vehicle.</param>
/// <param name="CapacityKg">The capacity in kilograms.</param>
public record RegisterVehicleResource(
    int OrganizationId,
    string Plate,
    decimal CapacityKg);