namespace Rutana.API.Planning.Interfaces.REST.Resources;

/// <summary>
/// Available location resource for REST API responses.
/// Simplified resource for locations available to create deliveries.
/// </summary>
/// <param name="Id">The unique identifier of the location.</param>
/// <param name="Address">The location address.</param>
/// <param name="Latitude">The latitude coordinate.</param>
/// <param name="Longitude">The longitude coordinate.</param>
public record AvailableLocationResource(
    int Id,
    string Address,
    string Latitude,
    string Longitude);

