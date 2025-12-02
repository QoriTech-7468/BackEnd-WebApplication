using Microsoft.EntityFrameworkCore;
using Rutana.API.Planning.Domain.Repositories;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using RouteDraftAggregate = Rutana.API.Planning.Domain.Model.Aggregates.RouteDraft;

namespace Rutana.API.Planning.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Repository implementation for managing RouteDraft aggregate persistence.
/// </summary>
/// <param name="context">
/// The <see cref="AppDbContext"/> to use.
/// </param>
public class RouteDraftRepository(AppDbContext context) 
    : BaseRepository<RouteDraftAggregate>(context), IRouteDraftRepository
{
    /// <inheritdoc />
    public override async Task<RouteDraftAggregate?> FindByIdAsync(int id)
    {
        // Include related entities (Deliveries and TeamMembers)
        return await Context.Set<RouteDraftAggregate>()
            .Include(rd => rd.Deliveries)
            .Include(rd => rd.TeamMembers)
            .FirstOrDefaultAsync(rd => rd.Id == id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RouteDraftAggregate>> FindByOrganizationIdAsync(OrganizationId organizationId)
    {
        // OrganizationId has HasConversion - must compare the value object directly, not .Value
        return await Context.Set<RouteDraftAggregate>()
            .Include(rd => rd.Deliveries)
            .Include(rd => rd.TeamMembers)
            .Where(rd => rd.OrganizationId == organizationId)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByIdAsync(int routeDraftId)
    {
        return await Context.Set<RouteDraftAggregate>()
            .AnyAsync(rd => rd.Id == routeDraftId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RouteDraftAggregate>> FindByExecutionDateAsync(OrganizationId organizationId, DateTime executionDate)
    {
        // Compare only the date part (ignore time)
        // Since ExecutionDate is stored as 'date' type in DB, we can compare directly
        // Normalize the input date to ensure only date part is used
        // OrganizationId has HasConversion - must compare the value object directly, not .Value
        var dateOnly = executionDate.Date;
        return await Context.Set<RouteDraftAggregate>()
            .Where(rd => 
                rd.OrganizationId == organizationId &&
                rd.ExecutionDate.Date == dateOnly.Date)
            .ToListAsync();
    }
}