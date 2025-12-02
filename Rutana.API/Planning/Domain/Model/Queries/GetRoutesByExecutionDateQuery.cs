namespace Rutana.API.Planning.Domain.Model.Queries;

/// <summary>
/// Query to get all routes for a specific execution date and organization.
/// </summary>
/// <param name="OrganizationId">The organization identifier.</param>
/// <param name="ExecutionDate">The execution date to filter by.</param>
public record GetRoutesByExecutionDateQuery(int OrganizationId, DateTime ExecutionDate);

