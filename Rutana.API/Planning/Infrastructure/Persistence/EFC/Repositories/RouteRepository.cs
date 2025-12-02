using Microsoft.EntityFrameworkCore;
using Rutana.API.Planning.Domain.Model.ValueObjects;
using Rutana.API.Planning.Domain.Repositories;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using RouteAggregate = Rutana.API.Planning.Domain.Model.Aggregates.Route;
using RouteDraftAggregate = Rutana.API.Planning.Domain.Model.Aggregates.RouteDraft;

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

    /// <inheritdoc />
    public async Task<IEnumerable<int>> GetUsedLocationIdsByExecutionDateAsync(OrganizationId organizationId, DateTime executionDate)
    {
        // Get LocationIds from deliveries in Routes with the specified executionDate and organizationId
        var dateOnly = executionDate.Date;
        
        // Query 1: Get LocationIds from Route deliveries
        var routeLocationIds = await Context.Set<RouteAggregate>()
            .Where(r => r.OrganizationId == organizationId && r.ExecutionDate.Date == dateOnly.Date)
            .SelectMany(r => r.Deliveries)
            .Where(d => d.ExecutionDate.Date == dateOnly.Date)
            .Select(d => d.LocationId.Value)
            .Distinct()
            .ToListAsync();

        // Query 2: Get LocationIds from RouteDraft deliveries (executed sequentially to avoid DbContext concurrency issues)
        var routeDraftLocationIds = await Context.Set<RouteDraftAggregate>()
            .Where(rd => rd.OrganizationId == organizationId && rd.ExecutionDate.Date == dateOnly.Date)
            .SelectMany(rd => rd.Deliveries)
            .Where(d => d.ExecutionDate.Date == dateOnly.Date)
            .Select(d => d.LocationId.Value)
            .Distinct()
            .ToListAsync();

        // Combine and return unique location IDs
        return routeLocationIds.Union(routeDraftLocationIds).Distinct();
    }
}