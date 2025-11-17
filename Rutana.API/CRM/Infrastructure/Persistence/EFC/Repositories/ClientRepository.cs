using Microsoft.EntityFrameworkCore;
using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Domain.Repositories;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Rutana.API.CRM.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Repository implementation for managing Client aggregate persistence.
/// </summary>
/// <param name="context">The database context.</param>
public class ClientRepository(AppDbContext context) : BaseRepository<Client>(context), IClientRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Client>> FindByOrganizationIdAsync(int organizationId)
    {
        // OrganizationId has HasConversion - must compare the value object directly, not .Value
        var orgId = new OrganizationId(organizationId);
        return await Context.Set<Client>()
            .Where(c => c.OrganizationId == orgId)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByCompanyNameAndOrganizationIdAsync(string companyName, int organizationId)
    {
        // OrganizationId has HasConversion - must compare the value object directly, not .Value
        var orgId = new OrganizationId(organizationId);
        return await Context.Set<Client>()
            .AnyAsync(c => c.CompanyName.Value == companyName && c.OrganizationId == orgId);
    }
}