namespace Rutana.API.IAM.Interfaces.REST.Resources;

/// <summary>
///     Resource representing an invitation.
/// </summary>
public record InvitationResource(
    int Id, 
    int OrganizationId, 
    int UserId, 
    string Role, 
    string Status, 
    DateTimeOffset CreatedAt
);

