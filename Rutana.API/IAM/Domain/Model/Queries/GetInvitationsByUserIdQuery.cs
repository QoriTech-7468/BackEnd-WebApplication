namespace Rutana.API.IAM.Domain.Model.Queries;

/// <summary>
///     Query to retrieve all invitations for a specific user.
/// </summary>
/// <param name="UserId">The identifier of the user.</param>
public record GetInvitationsByUserIdQuery(int UserId);

