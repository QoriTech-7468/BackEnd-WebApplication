namespace Rutana.API.CRM.Interfaces.REST.Resources;

/// <summary>
/// Client with locations resource for REST API responses.
/// Includes the client and all its locations.
/// </summary>
/// <param name="Id">The unique identifier of the client.</param>
/// <param name="Name">The name of the client.</param>
/// <param name="IsActive">Indicates whether the client is active.</param>
/// <param name="Locations">The list of locations belonging to this client.</param>
public record ClientWithLocationsResource(
    int Id,
    string Name,
    bool IsActive,
    IEnumerable<LocationResource> Locations);