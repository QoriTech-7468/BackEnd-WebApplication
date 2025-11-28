namespace Rutana.API.IAM.Interfaces.REST.Resources;

/// <summary>
///     Resource representing a summary of an invitation (for listing).
/// </summary>
public record InvitationSummaryResource(
    int Id, 
    int OrganizationId, 
    string Role, 
    string Status, 
    DateTimeOffset CreatedAt
);

