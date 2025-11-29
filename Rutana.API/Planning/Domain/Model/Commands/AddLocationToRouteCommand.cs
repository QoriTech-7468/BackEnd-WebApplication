namespace Rutana.API.Planning.Domain.Model.Commands;

/// <summary>
/// Command to add a location to a route draft.
/// </summary>
/// <param name="RouteDraftId">The route draft identifier.</param>
/// <param name="LocationId">The location identifier to add.</param>
public record AddLocationToRouteCommand(int RouteDraftId, int LocationId);