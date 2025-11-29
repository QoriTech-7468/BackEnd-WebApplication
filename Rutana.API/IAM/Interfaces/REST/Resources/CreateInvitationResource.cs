namespace Rutana.API.IAM.Interfaces.REST.Resources;

/// <summary>
///     Resource for creating a new invitation.
/// </summary>
public record CreateInvitationResource(string UserEmail, string Role);

