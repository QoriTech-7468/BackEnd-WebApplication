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
    public override async Task<Location?> FindByIdAsync(int locationId)
    {
        // Convert int to LocationId and use FindAsync which works correctly with value object primary keys
        var locationIdVo = new LocationId(locationId);
        return await Context.Set<Location>().FindAsync(locationIdVo);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Location>> FindByClientIdAsync(ClientId clientId)
    {
        // ClientId has HasConversion - must compare the value object directly, not .Value
        return await Context.Set<Location>()
            .Where(l => l.ClientId == clientId)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByLocationNameAndClientIdAsync(string locationName, int clientId)
    {
        // Note: This method is deprecated since we now use Address instead of Name
        // Keeping for backward compatibility but checking Address instead
        // ClientId has HasConversion - must compare the value object directly, not .Value
        var clientIdVo = new ClientId(clientId);
        return await Context.Set<Location>()
            .AnyAsync(l => l.Address.Value == locationName && l.ClientId == clientIdVo);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Location>> FindAllAsync(bool? isActive = null, int? clientId = null)
    {
        var query = Context.Set<Location>().AsQueryable();

        if (isActive.HasValue)
        {
            query = query.Where(l => l.IsEnabled == isActive.Value);
        }

        if (clientId.HasValue)
        {
            // ClientId has HasConversion - must compare the value object directly, not .Value
            var clientIdVo = new ClientId(clientId.Value);
            query = query.Where(l => l.ClientId == clientIdVo);
        }

        return await query.ToListAsync();
    }
}