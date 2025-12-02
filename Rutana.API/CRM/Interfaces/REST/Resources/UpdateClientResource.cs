namespace Rutana.API.CRM.Interfaces.REST.Resources;

/// <summary>
/// Resource to update a client.
/// </summary>
/// <param name="Id">The client identifier.</param>
/// <param name="Name">The name of the client.</param>
/// <param name="IsActive">Indicates whether the client is active.</param>
public record UpdateClientResource(
    int Id,
    string Name,
    bool IsActive);

