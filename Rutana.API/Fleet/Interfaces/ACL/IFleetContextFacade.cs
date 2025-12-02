namespace Rutana.API.Fleet.Interfaces.ACL;

/// <summary>
/// Fleet Context Facade interface for external bounded contexts.
/// Exposes Fleet capabilities to other bounded contexts through the Anti-Corruption Layer.
/// </summary>
public interface IFleetContextFacade
{
    /// <summary>
    /// Checks if a vehicle exists by its identifier.
    /// </summary>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <returns>True if the vehicle exists; otherwise, false.</returns>
    Task<bool> ExistsVehicleByIdAsync(int vehicleId);

    /// <summary>
    /// Checks if a vehicle is enabled.
    /// </summary>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <returns>True if the vehicle is enabled; otherwise, false.</returns>
    Task<bool> IsVehicleEnabledAsync(int vehicleId);

    /// <summary>
    /// Gets the vehicle capacity in kilograms.
    /// </summary>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <returns>The vehicle capacity in kg, or null if not found.</returns>
    Task<decimal?> GetVehicleCapacityKgAsync(int vehicleId);

    /// <summary>
    /// Gets the organization identifier for a given vehicle.
    /// </summary>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <returns>The organization identifier if found; otherwise, null.</returns>
    Task<int?> GetOrganizationIdByVehicleIdAsync(int vehicleId);
}