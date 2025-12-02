using Rutana.API.Shared.Domain.Model.ValueObjects;

namespace Rutana.API.IAM.Domain.Model.Queries;

/// <summary>
/// Query object used to request users by organization identifier
/// </summary>
/// <param name="OrganizationId">The identifier of the organization</param>
public record GetUsersByOrganizationIdQuery(int OrganizationId);

