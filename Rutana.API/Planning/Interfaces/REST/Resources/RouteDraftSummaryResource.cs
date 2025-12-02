namespace Rutana.API.Planning.Interfaces.REST.Resources;

/// <summary>
/// Simplified route draft resource for listing route drafts by execution date.
/// </summary>
/// <param name="Id">The unique identifier of the route draft.</param>
/// <param name="ColorCode">The color code for route identification.</param>
public record RouteDraftSummaryResource(
    int Id,
    string ColorCode);

