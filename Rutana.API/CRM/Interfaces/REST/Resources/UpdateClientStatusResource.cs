namespace Rutana.API.CRM.Interfaces.REST.Resources;

/// <summary>
/// Resource to update a client's status.
/// </summary>
/// <param name="IsActive">The new active status (true/false).</param>
public record UpdateClientStatusResource(bool IsActive);

