namespace Rutana.API.CRM.Interfaces.REST.Resources;

/// <summary>
/// Resource for registering a new client.
/// </summary>
/// <param name="OrganizationId">The organization that will own the client.</param>
/// <param name="CompanyName">The company name of the client.</param>
public record RegisterClientResource(
    int OrganizationId,
    string CompanyName);