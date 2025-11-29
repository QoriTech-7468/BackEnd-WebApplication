namespace Rutana.API.IAM.Interfaces.REST.Resources;

/// <summary>
///     Resource representing an invitation.
/// </summary>
public record InvitationResource(
    int Id, 
    string OrganizationName, 
    int UserId, 
    string Role, 
    string Status, 
    DateTimeOffset CreatedAt
);

