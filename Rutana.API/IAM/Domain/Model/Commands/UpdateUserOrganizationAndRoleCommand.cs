using Rutana.API.IAM.Domain.Model.Enums;

namespace Rutana.API.IAM.Domain.Model.Commands;

/// <summary>
///     Command to update a user's organization and role.
/// </summary>
/// <param name="UserId">The user identifier.</param>
/// <param name="OrganizationId">The organization identifier.</param>
/// <param name="Role">The new role.</param>
public record UpdateUserOrganizationAndRoleCommand(int UserId, int OrganizationId, UserRole Role);

