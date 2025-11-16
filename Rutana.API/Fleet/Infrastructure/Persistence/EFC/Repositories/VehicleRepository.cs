using Microsoft.EntityFrameworkCore;
using Rutana.API.Fleet.Domain.Model.Aggregates;
using Rutana.API.Fleet.Domain.Model.ValueObjects;
using Rutana.API.Fleet.Domain.Repositories;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Rutana.API.Fleet.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Repository implementation for managing Vehicle aggregate persistence.
/// </summary>
/// <param name="context">
/// The <see cref="AppDbContext"/> to use.
/// </param>
public class VehicleRepository(AppDbContext context) : BaseRepository<Vehicle>(context), IVehicleRepository
{
    /// <inheritdoc />
    public async Task<Vehicle?> FindByPlateAndOrganizationIdAsync(string plate, int organizationId)
    {
        return await Context.Set<Vehicle>()
            .FirstOrDefaultAsync(v => v.Plate.Value == plate && v.OrganizationId.Value == organizationId);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByPlateAndOrganizationIdAsync(string plate, int organizationId)
    {
        return await Context.Set<Vehicle>()
            .AnyAsync(v => v.Plate.Value == plate && v.OrganizationId.Value == organizationId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Vehicle>> FindByOrganizationIdAsync(int organizationId)
    {
        return await Context.Set<Vehicle>()
            .Where(v => v.OrganizationId.Value == organizationId)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Vehicle>> FindEnabledByOrganizationIdAsync(int organizationId)
    {
        return await Context.Set<Vehicle>()
            .Where(v => v.OrganizationId.Value == organizationId && v.State == VehicleState.Enabled)
            .ToListAsync();
    }
}