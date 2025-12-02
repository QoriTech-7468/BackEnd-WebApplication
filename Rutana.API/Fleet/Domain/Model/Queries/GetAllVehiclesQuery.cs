namespace Rutana.API.Fleet.Domain.Model.Queries;

/// <summary>
/// Query to get all vehicles filtered by the user's organization.
/// </summary>
/// <param name="OrganizationId">The mandatory organization identifier from the authenticated user.</param>
public record GetAllVehiclesQuery(int OrganizationId);