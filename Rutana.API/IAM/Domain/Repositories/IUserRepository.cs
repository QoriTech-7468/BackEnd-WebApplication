using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Shared.Domain.Repositories;

namespace Rutana.API.IAM.Domain.Repositories;

/// <summary>
///     Repository interface for <see cref="User" /> aggregates.
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    ///     Find a user by email asynchronously.
    /// </summary>
    /// <param name="email">The email to search.</param>
    /// <returns>The <see cref="User" /> when found; otherwise <c>null</c>.</returns>
    Task<User?> FindByUsernameAsync(string email);

    /// <summary>
    ///     Checks if a user exists by email.
    /// </summary>
    /// <param name="email">The username to check.</param>
    /// <returns><c>true</c> if a user with the given email exists; otherwise <c>false</c>.</returns>
    Task<bool> ExistsByUsername(string email);

    /// <summary>
    ///     Find all users by organization identifier asynchronously.
    /// </summary>
    /// <param name="organizationId">The organization identifier to search.</param>
    /// <returns>A collection of <see cref="User" /> instances belonging to the organization.</returns>
    Task<IEnumerable<User>> FindByOrganizationIdAsync(OrganizationId organizationId);
}