namespace Rutana.API.CRM.Interfaces.REST.Resources;

/// <summary>
/// Location resource for REST API responses.
/// </summary>
/// <param name="Id">The unique identifier of the location.</param>
/// <param name="Latitude">The latitude coordinate.</param>
/// <param name="Longitude">The longitude coordinate.</param>
/// <param name="Address">The address of the location.</param>
/// <param name="Proximity">The proximity level (close/mid/far).</param>
/// <param name="IsActive">Indicates whether the location is active.</param>
/// <param name="ClientId">The client identifier that owns this location.</param>
public record LocationResource(
    int Id,
    string Latitude,
    string Longitude,
    string Address,
    string Proximity,
    bool IsActive,
    int ClientId);