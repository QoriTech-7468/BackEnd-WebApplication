namespace Rutana.API.CRM.Interfaces.ACL;

/// <summary>
/// CRM Context Facade interface for external bounded contexts.
/// Exposes CRM capabilities to other bounded contexts through the Anti-Corruption Layer.
/// </summary>
public interface ICrmContextFacade
{
    /// <summary>
    /// Checks if a client exists by its identifier.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <returns>True if the client exists; otherwise, false.</returns>
    Task<bool> ExistsClientByIdAsync(int clientId);

    /// <summary>
    /// Checks if a location exists by its identifier.
    /// </summary>
    /// <param name="locationId">The location identifier.</param>
    /// <returns>True if the location exists; otherwise, false.</returns>
    Task<bool> ExistsLocationByIdAsync(int locationId);

    /// <summary>
    /// Gets the client identifier for a given location.
    /// </summary>
    /// <param name="locationId">The location identifier.</param>
    /// <returns>The client identifier if found; otherwise, null.</returns>
    Task<int?> GetClientIdByLocationIdAsync(int locationId);

    /// <summary>
    /// Checks if a location is enabled.
    /// </summary>
    /// <param name="locationId">The location identifier.</param>
    /// <returns>True if the location is enabled; otherwise, false.</returns>
    Task<bool> IsLocationEnabledAsync(int locationId);
}