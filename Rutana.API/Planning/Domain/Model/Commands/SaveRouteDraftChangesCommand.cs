namespace Rutana.API.Planning.Domain.Model.Commands;

/// <summary>
/// Command to save changes to a route draft.
/// </summary>
/// <param name="RouteDraftId">The route draft identifier.</param>
/// <param name="LocationIds">The list of location identifiers.</param>
/// <param name="TeamMemberIds">The list of team member user identifiers.</param>
/// <param name="VehicleId">The vehicle identifier.</param>
/// <param name="ColorCode">The color code for route identification.</param>
/// <param name="StartedAt">The planned start time.</param>
/// <param name="EndedAt">The planned end time.</param>
public record SaveRouteDraftChangesCommand(
    int RouteDraftId,
    IList<int>? LocationIds,
    IList<int>? TeamMemberIds,
    int? VehicleId,
    string? ColorCode,
    DateTime? StartedAt,
    DateTime? EndedAt);