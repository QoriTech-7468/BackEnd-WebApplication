namespace Rutana.API.Planning.Interfaces.REST.Resources;

/// <summary>
/// Route resource for REST API responses.
/// </summary>
/// <param name="Id">The unique identifier of the route.</param>
/// <param name="OrganizationId">The organization that owns the route.</param>
/// <param name="VehicleId">The vehicle assigned to the route.</param>
/// <param name="ColorCode">The color code for route identification.</param>
/// <param name="ExecutionDate">The date when the route will be executed.</param>
/// <param name="StartedAt">The start time of the route.</param>
/// <param name="EndedAt">The end time of the route.</param>
/// <param name="Status">The current status of the route.</param>
/// <param name="Deliveries">The list of deliveries.</param>
/// <param name="TeamMembers">The list of team members.</param>
public record RouteResource(
    int Id,
    int OrganizationId,
    int VehicleId,
    string ColorCode,
    DateTime ExecutionDate,
    DateTime StartedAt,
    DateTime? EndedAt,
    string Status,
    IEnumerable<DeliveryResource> Deliveries,
    IEnumerable<RouteTeamMemberResource> TeamMembers);