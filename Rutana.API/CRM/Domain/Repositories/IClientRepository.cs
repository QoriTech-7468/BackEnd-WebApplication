using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.Shared.Domain.Repositories;

namespace Rutana.API.CRM.Domain.Repositories;

/// <summary>
/// Repository interface for managing Client aggregate persistence.
/// </summary>
public interface IClientRepository : IBaseRepository<Client>
{
    /// <summary>
    /// Finds all clients belonging to an organization.
    /// </summary>
    /// <param name="organizationId">The organization identifier.</param>
    /// <returns>A collection of clients.</returns>
    Task<IEnumerable<Client>> FindByOrganizationIdAsync(int organizationId);

    /// <summary>
    /// Checks if a client with the given company name exists in the organization.
    /// </summary>
    /// <param name="companyName">The company name to check.</param>
    /// <param name="organizationId">The organization identifier.</param>
    /// <returns>True if exists, otherwise false.</returns>
    Task<bool> ExistsByCompanyNameAndOrganizationIdAsync(string companyName, int organizationId);
}