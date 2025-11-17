namespace Rutana.API.CRM.Interfaces.REST.Resources;

/// <summary>
/// Client with locations resource for REST API responses.
/// Includes the client and all its locations.
/// </summary>
/// <param name="Id">The unique identifier of the client.</param>
/// <param name="CompanyName">The company name of the client.</param>
/// <param name="IsEnabled">Indicates whether the client is enabled.</param>
/// <param name="Locations">The list of locations belonging to this client.</param>
public record ClientWithLocationsResource(
    int Id,
    string CompanyName,
    bool IsEnabled,
    IEnumerable<LocationResource> Locations);