namespace Rutana.API.Planning.Interfaces.REST.Resources;

/// <summary>
/// Resource for adding a location to a route draft.
/// </summary>
/// <param name="LocationId">The location identifier to add.</param>
public record AddLocationToRouteResource(int LocationId);