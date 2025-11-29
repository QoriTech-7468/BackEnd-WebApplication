namespace Rutana.API.IAM.Domain.Model.Queries;

/// <summary>
///     Query to retrieve a user by email address.
/// </summary>
/// <param name="Email">The email address of the user to retrieve.</param>
public record GetUserByEmailQuery(string Email);

