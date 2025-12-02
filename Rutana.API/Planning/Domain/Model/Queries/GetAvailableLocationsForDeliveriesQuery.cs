namespace Rutana.API.Planning.Domain.Model.Queries;

/// <summary>
/// Query to get available locations for creating deliveries on a specific execution date.
/// </summary>
/// <param name="OrganizationId">The organization identifier.</param>
/// <param name="ExecutionDate">The execution date to filter by.</param>
public record GetAvailableLocationsForDeliveriesQuery(int OrganizationId, DateTime ExecutionDate);

