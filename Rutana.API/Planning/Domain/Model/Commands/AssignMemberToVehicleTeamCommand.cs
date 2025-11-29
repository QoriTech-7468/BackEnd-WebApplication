namespace Rutana.API.Planning.Domain.Model.Commands;

/// <summary>
/// Command to assign a team member to a route draft.
/// </summary>
/// <param name="RouteDraftId">The route draft identifier.</param>
/// <param name="UserId">The user identifier to assign.</param>
public record AssignMemberToVehicleTeamCommand(int RouteDraftId, int UserId);