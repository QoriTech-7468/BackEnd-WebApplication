namespace Rutana.API.Planning.Domain.Model.Queries;

/// <summary>
/// Query to get a route draft by its identifier.
/// </summary>
/// <param name="RouteDraftId">The route draft identifier.</param>
public record GetRouteDraftByIdQuery(int RouteDraftId);