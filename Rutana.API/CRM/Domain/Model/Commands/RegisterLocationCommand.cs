using Rutana.API.CRM.Domain.Model.ValueObjects;

namespace Rutana.API.CRM.Domain.Model.Commands;

/// <summary>
/// Command to register a new location.
/// </summary>
/// <param name="ClientId">The client that owns the location.</param>
/// <param name="Name">The name of the location.</param>
/// <param name="Proximity">The proximity level of the location.</param>
public record RegisterLocationCommand(
    int ClientId,
    string Name,
    Proximity Proximity);