namespace Rutana.API.Planning.Interfaces.REST.Resources;

/// <summary>
/// Resource for saving changes to a route draft.
/// </summary>
/// <param name="ColorCode">The color code for route identification.</param>
/// <param name="VehicleId">The vehicle identifier.</param>
/// <param name="ExecutionDate">The date when the route will be executed.</param>
/// <param name="StartedAt">The planned start time.</param>
/// <param name="EndedAt">The planned end time.</param>
/// <param name="LocationIds">The list of location identifiers.</param>
/// <param name="TeamMemberIds">The list of team member user identifiers.</param>
public record SaveRouteDraftChangesResource(
    string? ColorCode,
    int? VehicleId,
    DateTime? ExecutionDate,
    DateTime? StartedAt,
    DateTime? EndedAt,
    IList<int>? LocationIds,
    IList<int>? TeamMemberIds);