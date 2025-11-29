using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.IAM.Domain.Model.Queries;

namespace Rutana.API.IAM.Domain.Services;

/// <summary>
///     Contract for querying invitation information.
/// </summary>
public interface IInvitationQueryService
{
    /// <summary>
    ///     Handle a <see cref="GetInvitationByIdQuery" /> to retrieve an invitation by id.
    /// </summary>
    Task<Invitation?> Handle(GetInvitationByIdQuery query);

    /// <summary>
    ///     Handle a <see cref="GetInvitationsByUserIdQuery" /> to retrieve all invitations for a user.
    /// </summary>
    Task<IEnumerable<Invitation>> Handle(GetInvitationsByUserIdQuery query);

    /// <summary>
    ///     Handle a <see cref="GetInvitationsByOrganizationIdQuery" /> to retrieve all invitations sent by an organization.
    /// </summary>
    Task<IEnumerable<Invitation>> Handle(GetInvitationsByOrganizationIdQuery query);
}

