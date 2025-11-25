namespace Rutana.API.Planning.Domain.Model.Commands;

/// <summary>
/// Command to delete a route draft.
/// </summary>
/// <param name="RouteDraftId">The route draft identifier to delete.</param>
public record DeleteRouteDraftCommand(int RouteDraftId);