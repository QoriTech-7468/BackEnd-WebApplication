using Rutana.API.Shared.Domain.Model.ValueObjects;

namespace Rutana.API.Suscriptions.Domain.Model.Queries;

/// <summary>
/// Get Organization by Id query.
/// </summary>
/// <param name="Id">The <see cref="OrganizationId" /> of the organization to retrieve.</param>
public record GetOrganizationByIdQuery(OrganizationId Id);


