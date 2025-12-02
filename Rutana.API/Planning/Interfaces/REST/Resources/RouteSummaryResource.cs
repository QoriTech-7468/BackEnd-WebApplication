namespace Rutana.API.Planning.Interfaces.REST.Resources;

/// <summary>
/// Simplified route resource for listing routes by execution date.
/// </summary>
/// <param name="Id">The unique identifier of the route.</param>
/// <param name="ColorCode">The color code for route identification.</param>
/// <param name="StartedAt">The start time of the route.</param>
/// <param name="EndedAt">The end time of the route.</param>
/// <param name="Status">The current status of the route.</param>
public record RouteSummaryResource(
    int Id,
    string ColorCode,
    DateTime StartedAt,
    DateTime? EndedAt,
    string Status);

