namespace Rutana.API.IAM.Domain.Model.Queries;

/// <summary>
///     Query to retrieve all invitations sent by an organization.
/// </summary>
/// <param name="OrganizationId">The identifier of the organization.</param>
public record GetInvitationsByOrganizationIdQuery(int OrganizationId);

