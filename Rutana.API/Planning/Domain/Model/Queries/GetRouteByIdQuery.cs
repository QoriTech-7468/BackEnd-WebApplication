namespace Rutana.API.Planning.Domain.Model.Queries;

/// <summary>
/// Query to get a route by its identifier.
/// </summary>
/// <param name="RouteId">The route identifier.</param>
public record GetRouteByIdQuery(int RouteId);