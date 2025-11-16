using Microsoft.EntityFrameworkCore;
using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Domain.Repositories;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Rutana.API.CRM.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Repository implementation for managing Location aggregate persistence.
/// </summary>
/// <param name="context">The database context.</param>
public class LocationRepository(AppDbContext context) : BaseRepository<Location>(context), ILocationRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Location>> FindByClientIdAsync(int clientId)
    {
        return await Context.Set<Location>()
            .Where(l => l.ClientId.Value == clientId)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByNameAndClientIdAsync(string name, int clientId)
    {
        return await Context.Set<Location>()
            .AnyAsync(l => l.Name.Value == name && l.ClientId.Value == clientId);
    }
}