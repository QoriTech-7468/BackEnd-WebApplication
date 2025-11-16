namespace Rutana.API.Fleet.Domain.Model.Commands;

/// <summary>
/// Command to register a new vehicle in the fleet.
/// </summary>
/// <param name="OrganizationId">The organization that owns the vehicle.</param>
/// <param name="Plate">The license plate number.</param>
/// <param name="CapacityKg">The vehicle capacity in kilograms.</param>
public record RegisterVehicleCommand(
    int OrganizationId,
    string Plate,
    decimal CapacityKg);