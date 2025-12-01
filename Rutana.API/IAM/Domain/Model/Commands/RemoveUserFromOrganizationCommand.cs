namespace Rutana.API.IAM.Domain.Model.Commands;

/// <summary>
///     Command to remove a user from an organization.
/// </summary>
/// <param name="UserId">The user identifier.</param>
public record RemoveUserFromOrganizationCommand(int UserId);

