using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.Shared.Domain.Repositories;

namespace Rutana.API.CRM.Domain.Repositories;

/// <summary>
/// Represents the location repository in the Rutana CRM System.
/// </summary>
public interface ILocationRepository : IBaseRepository<Location>
{
    /// <summary>
    /// Finds a location by its unique identifier.
    /// </summary>
    /// <param name="locationId">The location identifier.</param>
    /// <returns>The location if found; otherwise, null.</returns>
    Task<Location?> FindByIdAsync(int locationId);

    /// <summary>
    /// Finds all locations associated with a specific client.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <returns>A collection of locations.</returns>
    Task<IEnumerable<Location>> FindByClientIdAsync(ClientId clientId);

    /// <summary>
    /// Checks if a location with the specified name exists for a given client.
    /// </summary>
    /// <param name="locationName">The location name.</param>
    /// <param name="clientId">The client identifier.</param>
    /// <returns>True if exists; otherwise, false.</returns>
    Task<bool> ExistsByLocationNameAndClientIdAsync(string locationName, int clientId);

    /// <summary>
    /// Finds all locations, optionally filtered by active status and client ID.
    /// </summary>
    /// <param name="isActive">Optional filter by active status. If null, returns all locations.</param>
    /// <param name="clientId">Optional filter by client ID. If null, returns locations for all clients.</param>
    /// <returns>A collection of locations.</returns>
    Task<IEnumerable<Location>> FindAllAsync(bool? isActive = null, int? clientId = null);
}