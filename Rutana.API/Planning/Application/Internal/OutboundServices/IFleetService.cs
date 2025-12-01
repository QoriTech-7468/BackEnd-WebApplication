namespace Rutana.API.Planning.Application.Internal.OutboundServices;

/// <summary>
/// Outbound service interface for Fleet bounded context operations.
/// Provides Planning context with access to Fleet capabilities.
/// </summary>
public interface IFleetService
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