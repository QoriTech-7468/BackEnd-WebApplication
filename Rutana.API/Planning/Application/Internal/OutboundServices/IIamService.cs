namespace Rutana.API.Planning.Application.Internal.OutboundServices;

/// <summary>
/// Outbound service interface for IAM bounded context operations.
/// Provides Planning context with access to IAM capabilities.
/// </summary>
public interface IIamService
{
    /// <summary>
    /// Checks if a user exists by its identifier.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>True if the user exists; otherwise, false.</returns>
    Task<bool> ExistsUserByIdAsync(int userId);

    /// <summary>
    /// Gets the username (email) for a given user identifier.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>The username if found; otherwise, null.</returns>
    Task<string?> GetUsernameByUserIdAsync(int userId);
}