using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.Shared.Domain.Repositories;

namespace Rutana.API.CRM.Domain.Repositories;

/// <summary>
/// Repository interface for managing Location aggregate persistence.
/// </summary>
public interface ILocationRepository : IBaseRepository<Location>
{
    /// <summary>
    /// Finds all locations belonging to a client.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <returns>A collection of locations.</returns>
    Task<IEnumerable<Location>> FindByClientIdAsync(int clientId);

    /// <summary>
    /// Checks if a location with the given name exists for the client.
    /// </summary>
    /// <param name="name">The location name to check.</param>
    /// <param name="clientId">The client identifier.</param>
    /// <returns>True if exists, otherwise false.</returns>
    Task<bool> ExistsByNameAndClientIdAsync(string name, int clientId);
}
