using Rutana.API.CRM.Domain.Model.ValueObjects;

namespace Rutana.API.CRM.Domain.Model.Commands;

/// <summary>
/// Command to update a location.
/// </summary>
/// <param name="LocationId">The location identifier.</param>
/// <param name="Address">The address of the location.</param>
/// <param name="Latitude">The latitude coordinate of the location.</param>
/// <param name="Longitude">The longitude coordinate of the location.</param>
/// <param name="Proximity">The proximity level of the location.</param>
/// <param name="IsEnabled">The enabled status of the location.</param>
/// <param name="ClientId">The client identifier that owns the location.</param>
public record UpdateLocationCommand(
    LocationId LocationId,
    string Address,
    string Latitude,
    string Longitude,
    Proximity Proximity,
    bool IsEnabled,
    int ClientId);

