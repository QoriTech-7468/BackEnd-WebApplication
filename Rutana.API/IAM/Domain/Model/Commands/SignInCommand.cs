namespace Rutana.API.IAM.Domain.Model.Commands;



/// <summary>
///     Command used to authenticate a user (sign in).
/// </summary>
/// <param name="Email">The name to authenticate.</param>
/// <param name="Password">The plain-text password to authenticate.</param>
public record SignInCommand(string Email, string Password);