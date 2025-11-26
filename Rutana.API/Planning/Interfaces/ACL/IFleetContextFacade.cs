namespace Rutana.API.Planning.Interfaces.ACL;

/// <summary>
/// Facade for the Fleet context.
/// </summary>
public interface IFleetContextFacade
{
    /// <summary>
    /// Checks if a vehicle exists by its identifier.
    /// </summary>
    /// <param name="vehicleId">
    /// The vehicle identifier to check.
    /// </param>
    /// <returns>
    /// True if the vehicle exists, false otherwise.
    /// </returns>
    Task<bool> ExistsVehicleByIdAsync(int vehicleId);
    
    /// <summary>
    /// Checks if a vehicle is enabled.
    /// </summary>
    /// <param name="vehicleId">
    /// The vehicle identifier to check.
    /// </param>
    /// <returns>
    /// True if the vehicle is enabled, false otherwise.
    /// </returns>
    Task<bool> IsVehicleEnabledAsync(int vehicleId);
    
    /// <summary>
    /// Fetches the vehicle capacity in kilograms.
    /// </summary>
    /// <param name="vehicleId">
    /// The vehicle identifier.
    /// </param>
    /// <returns>
    /// The vehicle capacity in kg if found, null otherwise.
    /// </returns>
    Task<decimal?> FetchVehicleCapacityAsync(int vehicleId);
}