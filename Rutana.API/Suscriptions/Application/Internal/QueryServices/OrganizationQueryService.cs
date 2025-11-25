using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.Suscriptions.Domain.Model.Queries;
using Rutana.API.Suscriptions.Domain.Repositories;
using Rutana.API.Suscriptions.Domain.Services;

namespace Rutana.API.Suscriptions.Application.Internal.QueryServices;

/// <summary>
///     Application-level query service for the <see cref="Organization" /> aggregate.
/// </summary>
/// <param name="organizationRepository">
///     The <see cref="IOrganizationRepository" /> used to read <see cref="Organization" /> instances.
/// </param>
public class OrganizationQueryService(IOrganizationRepository organizationRepository)
    : IOrganizationQueryService
{
    /// <summary>
    ///     Handles the <see cref="GetOrganizationByIdQuery" /> by delegating to the
    ///     <see cref="IOrganizationRepository" /> and returning the matching organization, if any.
    /// </summary>
    /// <param name="query">
    ///     The query containing the <see cref="GetOrganizationByIdQuery.Id" /> to search for.
    /// </param>
    /// <returns>
    ///     The matching <see cref="Organization" /> if one exists; otherwise, <c>null</c>.
    /// </returns>
    public async Task<Organization?> Handle(GetOrganizationByIdQuery query)
    {
        // Under the hood, the repository uses the int-based primary key exposed via OrganizationId.Value.
        return await organizationRepository.FindByIdAsync(query.Id.Value);
    }
}
