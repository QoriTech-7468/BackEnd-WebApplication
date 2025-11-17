namespace Rutana.API.CRM.Interfaces.REST.Resources;

/// <summary>
/// Client summary resource for REST API responses (used within other resources).
/// </summary>
/// <param name="Id">The unique identifier of the client.</param>
/// <param name="CompanyName">The company name of the client.</param>
public record ClientSummaryResource(
    int Id,
    string CompanyName);