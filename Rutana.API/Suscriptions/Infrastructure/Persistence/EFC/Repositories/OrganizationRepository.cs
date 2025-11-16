using Microsoft.EntityFrameworkCore;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Rutana.API.Suscriptions.Domain.Repositories;
using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.Suscriptions.Domain.Model.ValueObjects;

namespace Rutana.API.Suscriptions.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Entity Framework Core implementation of <see cref="IOrganizationRepository" />.
/// </summary>
public class OrganizationRepository : BaseRepository<Organization>, IOrganizationRepository
{

    public async Task<Organization?> FindByRucAsync(Ruc ruc)
    {
        return await Context.Set<Organization>()
            .FirstOrDefaultAsync(o => o.Ruc.Value == ruc.Value);
    }

    public async Task<bool> ExistsByRucAsync(Ruc ruc)
    {
        return await Context.Set<Organization>()
            .AnyAsync(o => o.Ruc.Value == ruc.Value);
    }
}


