using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Shared.Domain.Repositories;
using RouteAggregate = Rutana.API.Planning.Domain.Model.Aggregates.Route; 

//ROUTE AGGREGATE - alias para evitar ambigüedades con otras clases llamadas Route de Microsoft.EntityFramework u otras librerías.

namespace Rutana.API.Planning.Domain.Repositories;

/// <summary>
/// Repository interface for managing Route aggregate persistence.
/// </summary>
public interface IRouteRepository : IBaseRepository<RouteAggregate>
{
    /// <summary>
    /// Finds all routes belonging to an organization.
    /// </summary>
    /// <param name="organizationId">The organization identifier value object.</param>
    /// <returns>A collection of routes.</returns>
    Task<IEnumerable<RouteAggregate>> FindByOrganizationIdAsync(OrganizationId organizationId);

    /// <summary>
    /// Finds all active (published) routes belonging to an organization.
    /// </summary>
    /// <param name="organizationId">The organization identifier value object.</param>
    /// <returns>A collection of active routes.</returns>
    Task<IEnumerable<RouteAggregate>> FindActiveByOrganizationIdAsync(OrganizationId organizationId);

    /// <summary>
    /// Finds all completed routes belonging to an organization.
    /// </summary>
    /// <param name="organizationId">The organization identifier value object.</param>
    /// <returns>A collection of completed routes.</returns>
    Task<IEnumerable<RouteAggregate>> FindCompletedByOrganizationIdAsync(OrganizationId organizationId);

    /// <summary>
    /// Finds all routes for a specific execution date and organization.
    /// </summary>
    /// <param name="organizationId">The organization identifier.</param>
    /// <param name="executionDate">The execution date to filter by.</param>
    /// <returns>A collection of routes.</returns>
    Task<IEnumerable<RouteAggregate>> FindByExecutionDateAsync(OrganizationId organizationId, DateTime executionDate);

    /// <summary>
    /// Gets all location IDs from deliveries that belong to routes or route drafts with the specified execution date and organization.
    /// </summary>
    /// <param name="organizationId">The organization identifier.</param>
    /// <param name="executionDate">The execution date to filter by.</param>
    /// <returns>A collection of unique location IDs.</returns>
    Task<IEnumerable<int>> GetUsedLocationIdsByExecutionDateAsync(OrganizationId organizationId, DateTime executionDate);
}