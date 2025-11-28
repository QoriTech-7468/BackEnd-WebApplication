namespace Rutana.API.IAM.Interfaces.REST.Resources;

/// <summary>
///     Resource representing user information retrieved by email.
/// </summary>
public record UserByEmailResource(int Id, string Email, string Name, string Surname);

