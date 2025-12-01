namespace Rutana.API.CRM.Interfaces.REST.Resources;

/// <summary>
/// Resource to update a location.
/// </summary>
/// <param name="Id">The location identifier.</param>
/// <param name="Address">The address of the location.</param>
/// <param name="Latitude">The latitude coordinate.</param>
/// <param name="Longitude">The longitude coordinate.</param>
/// <param name="Proximity">The proximity level (close/mid/far).</param>
/// <param name="IsActive">Indicates whether the location is active.</param>
/// <param name="ClientId">The client identifier that owns the location.</param>
public record UpdateLocationResource(
    int Id,
    string Address,
    string Latitude,
    string Longitude,
    string Proximity,
    bool IsActive,
    int ClientId);

