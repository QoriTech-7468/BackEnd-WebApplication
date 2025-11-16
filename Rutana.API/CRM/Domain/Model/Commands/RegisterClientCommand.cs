namespace Rutana.API.CRM.Domain.Model.Commands;

/// <summary>
/// Command to register a new client.
/// </summary>
/// <param name="OrganizationId">The organization that owns the client.</param>
/// <param name="CompanyName">The company name of the client.</param>
public record RegisterClientCommand(
    int OrganizationId,
    string CompanyName);