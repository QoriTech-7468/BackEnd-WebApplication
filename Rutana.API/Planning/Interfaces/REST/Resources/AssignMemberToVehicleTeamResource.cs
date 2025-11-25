namespace Rutana.API.Planning.Interfaces.REST.Resources;

/// <summary>
/// Resource for assigning a team member to a route draft.
/// </summary>
/// <param name="UserId">The user identifier to assign.</param>
public record AssignMemberToVehicleTeamResource(int UserId);