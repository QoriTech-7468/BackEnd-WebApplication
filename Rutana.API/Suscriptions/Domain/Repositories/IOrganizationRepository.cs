using Rutana.API.Shared.Domain.Repositories;
using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.Suscriptions.Domain.Model.ValueObjects;

namespace Rutana.API.Suscriptions.Domain.Repositories;

/// <summary>
/// Organization repository contract for the Subscriptions bounded context.
/// </summary>
public interface IOrganizationRepository : IBaseRepository<Organization>
{
    /// <summary>
    /// Finds an organization by its RUC.
    /// </summary>
    /// <param name="ruc">The <see cref="Ruc" /> value object.</param>
    /// <returns>The matching <see cref="Organization" /> if found; otherwise, null.</returns>
    Task<Organization?> FindByRucAsync(Ruc ruc);

    /// <summary>
    /// Checks whether an organization exists with the given RUC.
    /// </summary>
    /// <param name="ruc">The <see cref="Ruc" /> value object.</param>
    /// <returns><c>true</c> if an organization exists; otherwise, <c>false</c>.</returns>
    Task<bool> ExistsByRucAsync(Ruc ruc);
}


