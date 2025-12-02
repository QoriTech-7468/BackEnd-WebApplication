namespace Rutana.API.CRM.Interfaces.REST.Resources;

/// <summary>
/// Client resource for REST API responses.
/// </summary>
/// <param name="Id">The unique identifier of the client.</param>
/// <param name="Name">The name of the client.</param>
/// <param name="IsActive">Indicates whether the client is active.</param>
public record ClientResource(
    int Id,
    string Name,
    bool IsActive);