namespace Rutana.API.Planning.Domain.Model.Queries;

/// <summary>
/// Query to get all routes for a specific organization.
/// </summary>
/// <param name="OrganizationId">The organization identifier.</param>
public record GetRoutesByOrganizationIdQuery(int OrganizationId);