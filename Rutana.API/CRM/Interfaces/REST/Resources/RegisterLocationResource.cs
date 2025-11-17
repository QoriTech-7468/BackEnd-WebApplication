namespace Rutana.API.CRM.Interfaces.REST.Resources;

/// <summary>
/// Resource for registering a new location.
/// </summary>
/// <param name="ClientId">The client that will own the location.</param>
/// <param name="Name">The name of the location.</param>
/// <param name="Proximity">The proximity level (Near/Medium/Far).</param>
public record RegisterLocationResource(
    int ClientId,
    string Name,
    string Proximity);