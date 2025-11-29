namespace Rutana.API.Planning.Application.Internal.OutboundServices;

/// <summary>
/// Outbound service interface for CRM bounded context operations.
/// Provides Planning context with access to CRM capabilities.
/// </summary>
public interface ICrmService
{
    /// <summary>
    /// Checks if a location exists by its identifier.
    /// </summary>
    /// <param name="locationId">The location identifier.</param>
    /// <returns>True if the location exists; otherwise, false.</returns>
    Task<bool> ExistsLocationByIdAsync(int locationId);

    /// <summary>
    /// Checks if a location is enabled.
    /// </summary>
    /// <param name="locationId">The location identifier.</param>
    /// <returns>True if the location is enabled; otherwise, false.</returns>
    Task<bool> IsLocationEnabledAsync(int locationId);

    /// <summary>
    /// Gets the client identifier for a given location.
    /// </summary>
    /// <param name="locationId">The location identifier.</param>
    /// <returns>The client identifier if found; otherwise, null.</returns>
    Task<int?> GetClientIdByLocationIdAsync(int locationId);
}