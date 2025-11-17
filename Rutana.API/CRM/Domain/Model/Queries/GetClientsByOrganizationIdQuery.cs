namespace Rutana.API.CRM.Domain.Model.Queries;

/// <summary>
/// Query to get all clients belonging to an organization.
/// </summary>
/// <param name="OrganizationId">The identifier of the organization.</param>
public record GetClientsByOrganizationIdQuery(int OrganizationId);