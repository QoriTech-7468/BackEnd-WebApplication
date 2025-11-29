namespace Rutana.API.Planning.Interfaces.ACL;

/// <summary>
/// Facade for the CRM context.
/// </summary>
public interface ICrmContextFacade
{
    /// <summary>
    /// Checks if a location exists by its identifier.
    /// </summary>
    /// <param name="locationId">
    /// The location identifier to check.
    /// </param>
    /// <returns>
    /// True if the location exists, false otherwise.
    /// </returns>
    Task<bool> ExistsLocationByIdAsync(int locationId);
    
    /// <summary>
    /// Checks if a location is enabled.
    /// </summary>
    /// <param name="locationId">
    /// The location identifier to check.
    /// </param>
    /// <returns>
    /// True if the location is enabled, false otherwise.
    /// </returns>
    Task<bool> IsLocationEnabledAsync(int locationId);
    
    /// <summary>
    /// Fetches the client id that owns the location.
    /// </summary>
    /// <param name="locationId">
    /// The location identifier.
    /// </param>
    /// <returns>
    /// The client id if found, 0 otherwise.
    /// </returns>
    Task<int> FetchClientIdByLocationIdAsync(int locationId);
}