using Microsoft.EntityFrameworkCore;
using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.CRM.Domain.Repositories;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Rutana.API.CRM.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Location repository implementation using Entity Framework Core.
/// </summary>
/// <param name="context">The application database context.</param>
public class LocationRepository(AppDbContext context) 
    : BaseRepository<Location>(context), ILocationRepository
{
    /// <inheritdoc />
    public async Task<Location?> FindByIdAsync(int locationId)
    {
        return await Context.Set<Location>()
            .FirstOrDefaultAsync(l => l.Id.Value == locationId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Location>> FindByClientIdAsync(ClientId clientId)
    {
        return await Context.Set<Location>()
            .Where(l => l.ClientId.Value == clientId.Value)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByLocationNameAndClientIdAsync(string locationName, int clientId)
    {
        return await Context.Set<Location>()
            .AnyAsync(l => l.Name.Value == locationName && l.ClientId.Value == clientId);
    }
}