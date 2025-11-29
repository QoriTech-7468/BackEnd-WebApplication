using Rutana.API.IAM.Domain.Model.Enums;

namespace Rutana.API.IAM.Domain.Model.Commands;

/// <summary>
///     Command to create a new invitation.
/// </summary>
/// <param name="OrganizationId">The organization sending the invitation.</param>
/// <param name="UserEmail">The email of the user to invite.</param>
/// <param name="Role">The role to assign (Admin or Dispatcher only).</param>
public record CreateInvitationCommand(int OrganizationId, string UserEmail, UserRole Role);

