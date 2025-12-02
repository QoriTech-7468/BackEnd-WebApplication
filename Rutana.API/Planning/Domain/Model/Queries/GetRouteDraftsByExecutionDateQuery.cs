namespace Rutana.API.Planning.Domain.Model.Queries;

/// <summary>
/// Query to get all route drafts for a specific execution date and organization.
/// </summary>
/// <param name="OrganizationId">The organization identifier.</param>
/// <param name="ExecutionDate">The execution date to filter by.</param>
public record GetRouteDraftsByExecutionDateQuery(int OrganizationId, DateTime ExecutionDate);

