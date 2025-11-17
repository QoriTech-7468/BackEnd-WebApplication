namespace Rutana.API.Fleet.Interfaces.REST.Resources;

/// <summary>
/// Vehicle resource for REST API responses.
/// </summary>
/// <param name="Id">The unique identifier of the vehicle.</param>
/// <param name="OrganizationId">The organization that owns the vehicle.</param>
/// <param name="Plate">The license plate of the vehicle.</param>
/// <param name="CapacityKg">The capacity in kilograms.</param>
/// <param name="State">The current state of the vehicle (Enabled/Disabled).</param>
public record VehicleResource(
    int Id,
    int OrganizationId,
    string Plate,
    decimal CapacityKg,
    string State);