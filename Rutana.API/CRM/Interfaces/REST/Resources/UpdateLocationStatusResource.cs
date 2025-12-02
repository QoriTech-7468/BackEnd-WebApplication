namespace Rutana.API.CRM.Interfaces.REST.Resources;

/// <summary>
/// Resource to update a location's status.
/// </summary>
/// <param name="IsActive">The new active status (true/false).</param>
public record UpdateLocationStatusResource(bool IsActive);

