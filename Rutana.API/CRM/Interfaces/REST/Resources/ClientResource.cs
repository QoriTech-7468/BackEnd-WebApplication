namespace Rutana.API.CRM.Interfaces.REST.Resources;

/// <summary>
/// Client resource for REST API responses.
/// </summary>
/// <param name="Id">The unique identifier of the client.</param>
/// <param name="CompanyName">The company name of the client.</param>
/// <param name="IsEnabled">Indicates whether the client is enabled.</param>
public record ClientResource(
    int Id,
    string CompanyName,
    bool IsEnabled);