namespace Rutana.API.Planning.Interfaces.REST.Resources;

/// <summary>
/// Resource to update route draft status.
/// </summary>
/// <param name="Status">The new status (published).</param>
public record UpdateRouteDraftStatusResource(string Status);