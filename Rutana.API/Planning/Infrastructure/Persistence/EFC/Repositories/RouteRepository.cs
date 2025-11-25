using Microsoft.EntityFrameworkCore;
using Rutana.API.Planning.Domain.Model.ValueObjects;
using Rutana.API.Planning.Domain.Repositories;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using RouteAggregate = Rutana.API.Planning.Domain.Model.Aggregates.Route;

namespace Rutana.API.Planning.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Repository implementation for managing Route aggregate persistence.
/// </summary>
/// <param name="context">
/// The <see cref="AppDbContext"/> to use.
/// </param>
public class RouteRepository(AppDbContext context)
    : BaseRepository<RouteAggregate>(context), IRouteRepository
{
    /// <inheritdoc />
    public override async Task<RouteAggregate?> FindByIdAsync(int id)
    {
        // Include related entities (Deliveries and TeamMembers)
        return await Context.Set<RouteAggregate>()
            .Include(r => r.Deliveries)
            .Include(r => r.TeamMembers)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RouteAggregate>> FindByOrganizationIdAsync(OrganizationId organizationId)
    {
        // OrganizationId has HasConversion - must compare the value object directly, not .Value
        return await Context.Set<RouteAggregate>()
            .Include(r => r.Deliveries)
            .Include(r => r.TeamMembers)
            .Where(r => r.OrganizationId == organizationId)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RouteAggregate>> FindActiveByOrganizationIdAsync(OrganizationId organizationId)
    {
        // OrganizationId has HasConversion - must compare the value object directly, not .Value
        return await Context.Set<RouteAggregate>()
            .Include(r => r.Deliveries)
            .Include(r => r.TeamMembers)
            .Where(r =>
                r.OrganizationId == organizationId &&
                r.Status == RouteStatus.Published)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RouteAggregate>> FindCompletedByOrganizationIdAsync(OrganizationId organizationId)
    {
        // OrganizationId has HasConversion - must compare the value object directly, not .Value
        return await Context.Set<RouteAggregate>()
            .Include(r => r.Deliveries)
            .Include(r => r.TeamMembers)
            .Where(r =>
                r.OrganizationId == organizationId &&
                r.Status == RouteStatus.Completed)
            .ToListAsync();
    }
}