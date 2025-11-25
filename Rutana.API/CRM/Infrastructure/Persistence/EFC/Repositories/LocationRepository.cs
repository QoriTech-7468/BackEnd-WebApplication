using Microsoft.EntityFrameworkCore;
using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Domain.Model.ValueObjects;
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
    public override async Task<Location?> FindByIdAsync(int id)
    {
        // Convert int to LocationId and use FindAsync which works correctly with value object primary keys
        var locationId = new LocationId(id);
        return await Context.Set<Location>().FindAsync(locationId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Location>> FindByClientIdAsync(int clientId)
    {
        // ClientId has HasConversion - must compare the value object directly, not .Value
        var clientIdVo = new ClientId(clientId);
        return await Context.Set<Location>()
            .Where(l => l.ClientId == clientIdVo)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByNameAndClientIdAsync(string name, int clientId)
    {
        // ClientId has HasConversion - must compare the value object directly, not .Value
        var clientIdVo = new ClientId(clientId);
        return await Context.Set<Location>()
            .AnyAsync(l => l.Name.Value == name && l.ClientId == clientIdVo);
    }
}