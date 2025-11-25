namespace Rutana.API.Planning.Interfaces.REST.Resources;

/// <summary>
/// Route team member resource for REST API responses.
/// </summary>
/// <param name="Id">The unique identifier of the team member.</param>
/// <param name="UserId">The user identifier.</param>
public record RouteTeamMemberResource(
    int Id,
    int UserId);