using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.Suscriptions.Domain.Model.Queries;

namespace Rutana.API.Suscriptions.Domain.Application.Services;

/// <summary>
/// Contract for organization query handling at the application layer.
/// </summary>
public interface IOrganizationQueryService
{
    /// <summary>
    /// Handles the get organization by id query.
    /// </summary>
    /// <param name="query">The get organization by id query.</param>
    /// <returns>The matching <see cref="Organization" /> if found; otherwise, null.</returns>
    Task<Organization?> Handle(GetOrganizationByIdQuery query);
}


