namespace Rutana.API.Planning.Interfaces.REST.Resources;

/// <summary>
/// Route draft resource for REST API responses.
/// </summary>
/// <param name="Id">The unique identifier of the route draft.</param>
/// <param name="OrganizationId">The organization that owns the route draft.</param>
/// <param name="VehicleId">The vehicle assigned to the route draft.</param>
/// <param name="ColorCode">The color code for route identification.</param>
/// <param name="ExecutionDate">The date when the route will be executed.</param>
/// <param name="Deliveries">The list of deliveries.</param>
/// <param name="TeamMembers">The list of team members.</param>
public record RouteDraftResource(
    int Id,
    int OrganizationId,
    int? VehicleId,
    string ColorCode,
    DateTime ExecutionDate,
    IEnumerable<DeliveryResource> Deliveries,
    IEnumerable<RouteTeamMemberResource> TeamMembers);