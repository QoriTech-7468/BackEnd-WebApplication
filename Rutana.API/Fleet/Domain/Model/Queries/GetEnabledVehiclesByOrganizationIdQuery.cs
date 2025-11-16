namespace Rutana.API.Fleet.Domain.Model.Queries;

/// <summary>
/// Query to get enabled vehicles by organization ID.
/// </summary>
/// <param name="OrganizationId"></param>
public record GetEnabledVehiclesByOrganizationIdQuery(int OrganizationId);