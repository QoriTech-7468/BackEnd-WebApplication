namespace Rutana.API.IAM.Domain.Model.Queries;

/// <summary>
///     Query to retrieve an invitation by identifier.
/// </summary>
/// <param name="InvitationId">The identifier of the invitation to retrieve.</param>
public record GetInvitationByIdQuery(int InvitationId);

