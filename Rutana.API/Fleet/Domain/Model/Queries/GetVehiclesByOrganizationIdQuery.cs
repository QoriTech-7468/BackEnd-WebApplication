namespace Rutana.API.Fleet.Domain.Model.Queries;

/// <summary>
/// Query to get vehicles by organization ID.
/// </summary>
/// <param name="OrganizationId"></param>
public record GetVehiclesByOrganizationIdQuery(int OrganizationId);