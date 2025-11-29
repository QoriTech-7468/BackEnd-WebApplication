namespace Rutana.API.IAM.Interfaces.REST.Resources;

/// <summary>
///     Resource representing a summary of an invitation (for listing).
/// </summary>
public record InvitationSummaryResource(
    int Id, 
    string OrganizationName, 
    string Role, 
    string Status, 
    DateTimeOffset CreatedAt
);

