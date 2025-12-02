namespace Rutana.API.CRM.Interfaces.REST.Resources;

/// <summary>
/// Resource for registering a new location.
/// </summary>
/// <param name="ClientId">The client that will own the location.</param>
/// <param name="Address">The address of the location.</param>
/// <param name="Latitude">The latitude coordinate.</param>
/// <param name="Longitude">The longitude coordinate.</param>
/// <param name="Proximity">The proximity level (close/mid/far).</param>
/// <param name="IsActive">Indicates whether the location is active (default: true).</param>
public record RegisterLocationResource(
    int ClientId,
    string Address,
    string Latitude,
    string Longitude,
    string Proximity,
    bool IsActive = true);