using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Shared.Domain.Repositories;

namespace Rutana.API.IAM.Domain.Repositories;

/// <summary>
///     Repository interface for <see cref="Invitation" /> aggregates.
/// </summary>
public interface IInvitationRepository : IBaseRepository<Invitation>
{
    /// <summary>
    ///     Find an invitation by user ID and organization ID asynchronously.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="organizationId">The organization identifier.</param>
    /// <returns>The <see cref="Invitation" /> when found; otherwise <c>null</c>.</returns>
    Task<Invitation?> FindByUserIdAndOrganizationIdAsync(int userId, OrganizationId organizationId);

    /// <summary>
    ///     Find all pending invitations for a user asynchronously.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>A collection of pending <see cref="Invitation" /> instances.</returns>
    Task<IEnumerable<Invitation>> FindPendingByUserIdAsync(int userId);

    /// <summary>
    ///     Find all pending invitations sent by an organization asynchronously.
    /// </summary>
    /// <param name="organizationId">The organization identifier.</param>
    /// <returns>A collection of pending <see cref="Invitation" /> instances.</returns>
    Task<IEnumerable<Invitation>> FindPendingByOrganizationIdAsync(OrganizationId organizationId);
}

