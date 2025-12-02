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
        // Active routes are those that are NotStarted or InProgress (not Finished)
        return await Context.Set<RouteAggregate>()
            .Include(r => r.Deliveries)
            .Include(r => r.TeamMembers)
            .Where(r =>
                r.OrganizationId == organizationId &&
                (r.Status == RouteStatus.NotStarted || r.Status == RouteStatus.InProgress))
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
                r.Status == RouteStatus.Finished)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RouteAggregate>> FindByExecutionDateAsync(OrganizationId organizationId, DateTime executionDate)
    {
        // Compare only the date part (ignore time)
        // Since ExecutionDate is stored as 'date' type in DB, we can compare directly
        // Normalize the input date to ensure only date part is used
        // OrganizationId has HasConversion - must compare the value object directly, not .Value
        var dateOnly = executionDate.Date;
        return await Context.Set<RouteAggregate>()
            .Where(r => 
                r.OrganizationId == organizationId &&
                r.ExecutionDate.Date == dateOnly.Date)
            .ToListAsync();
    }
}