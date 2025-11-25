namespace Rutana.API.CRM.Interfaces.REST.Resources;

/// <summary>
/// Location resource for REST API responses.
/// </summary>
/// <param name="Id">The unique identifier of the location.</param>
/// <param name="Name">The name of the location.</param>
/// <param name="Proximity">The proximity level (Near/Medium/Far).</param>
/// <param name="IsEnabled">Indicates whether the location is enabled.</param>
/// <param name="Client">The client summary that owns this location.</param>
public record LocationResource(
    int Id,
    string Name,
    string Proximity,
    bool IsEnabled,
    ClientSummaryResource Client);