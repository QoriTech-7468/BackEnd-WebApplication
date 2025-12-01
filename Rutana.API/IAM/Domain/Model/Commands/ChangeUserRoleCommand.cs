using Rutana.API.IAM.Domain.Model.Enums;

namespace Rutana.API.IAM.Domain.Model.Commands;

/// <summary>
///     Command to change a user's role.
/// </summary>
/// <param name="UserId">The user identifier.</param>
/// <param name="Role">The new role (Admin or Dispatcher only).</param>
public record ChangeUserRoleCommand(int UserId, UserRole Role);

