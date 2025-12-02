using Rutana.API.Planning.Domain.Model.Aggregates;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Shared.Domain.Repositories;

namespace Rutana.API.Planning.Domain.Repositories;

/// <summary>
/// Repository interface for managing RouteDraft aggregate persistence.
/// </summary>
public interface IRouteDraftRepository : IBaseRepository<RouteDraft>
{
    /// <summary>
    /// Finds all route drafts belonging to an organization.
    /// </summary>
    /// <param name="organizationId">The organization identifier value object.</param>
    /// <returns>A collection of route drafts.</returns>
    Task<IEnumerable<RouteDraft>> FindByOrganizationIdAsync(OrganizationId organizationId);

    /// <summary>
    /// Checks if a route draft exists by its identifier.
    /// </summary>
    /// <param name="routeDraftId">The route draft identifier.</param>
    /// <returns>True if exists, otherwise false.</returns>
    Task<bool> ExistsByIdAsync(int routeDraftId);

    /// <summary>
    /// Finds all route drafts for a specific execution date and organization.
    /// </summary>
    /// <param name="organizationId">The organization identifier.</param>
    /// <param name="executionDate">The execution date to filter by.</param>
    /// <returns>A collection of route drafts.</returns>
    Task<IEnumerable<RouteDraft>> FindByExecutionDateAsync(OrganizationId organizationId, DateTime executionDate);
}