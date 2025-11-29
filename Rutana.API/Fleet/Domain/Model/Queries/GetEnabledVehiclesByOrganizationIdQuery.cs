namespace Rutana.API.Fleet.Domain.Model.Queries;

/// <summary>
/// Query to get enabled vehicles by organization id.
/// </summary>
/// <param name="OrganizationId">The organization identifier.</param>
public record GetEnabledVehiclesByOrganizationIdQuery(int OrganizationId);