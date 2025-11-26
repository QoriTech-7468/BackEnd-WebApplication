namespace Rutana.API.IAM.Domain.Model.Queries;

/// <summary>
/// Query object used to request a user by rol
/// </summary>
/// <param name="Role">The rol of the username in the organization</param>
public record GetUserByRoleQuery(string Role);