namespace Rutana.API.Planning.Domain.Model.Commands;

/// <summary>
/// Command to publish a route draft as an active route.
/// </summary>
/// <param name="RouteDraftId">The route draft identifier to publish.</param>
public record PublishRouteCommand(int RouteDraftId);