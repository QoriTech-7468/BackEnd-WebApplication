namespace Rutana.API.Planning.Domain.Model.Queries;

/// <summary>
/// Query to get all route drafts for a specific organization.
/// </summary>
/// <param name="OrganizationId">The organization identifier.</param>
public record GetRouteDraftsByOrganizationIdQuery(int OrganizationId);
