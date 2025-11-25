using Microsoft.EntityFrameworkCore;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Rutana.API.Suscriptions.Domain.Repositories;
using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.Suscriptions.Domain.Model.ValueObjects;

namespace Rutana.API.Suscriptions.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Repository implementation for managing Organization aggregate persistence.
/// </summary>
/// <param name="context">
/// The <see cref="AppDbContext"/> to use.
/// </param>
public class OrganizationRepository(AppDbContext context) : BaseRepository<Organization>(context), IOrganizationRepository
{
    /// <inheritdoc />
    public override async Task<Organization?> FindByIdAsync(int id)
    {
        // Convert int to OrganizationId and use FindAsync which works correctly with value object primary keys
        var organizationId = new OrganizationId(id);
        return await Context.Set<Organization>().FindAsync(organizationId);
    }

    /// <inheritdoc />
    public async Task<Organization?> FindByRucAsync(Ruc ruc)
    {
        return await Context.Set<Organization>()
            .FirstOrDefaultAsync(o => o.Ruc.Value == ruc.Value);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByRucAsync(Ruc ruc)
    {
        return await Context.Set<Organization>()
            .AnyAsync(o => o.Ruc.Value == ruc.Value);
    }
}


