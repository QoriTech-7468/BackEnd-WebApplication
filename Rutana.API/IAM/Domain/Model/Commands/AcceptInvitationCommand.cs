namespace Rutana.API.IAM.Domain.Model.Commands;

/// <summary>
///     Command to accept an invitation.
/// </summary>
/// <param name="InvitationId">The identifier of the invitation to accept.</param>
public record AcceptInvitationCommand(int InvitationId);

