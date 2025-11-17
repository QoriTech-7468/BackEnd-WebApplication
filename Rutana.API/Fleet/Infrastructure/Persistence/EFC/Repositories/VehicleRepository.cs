using Microsoft.EntityFrameworkCore;
using Rutana.API.Fleet.Domain.Model.Aggregates;
using Rutana.API.Fleet.Domain.Model.ValueObjects;
using Rutana.API.Fleet.Domain.Repositories;
using Rutana.API.Shared.Domain.Model.ValueObjects;
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
    public async Task<Vehicle?> FindByPlateAndOrganizationIdAsync(LicensePlate plate, OrganizationId organizationId)
    {
        // Plate is an owned type (OwnsOne) - can use .Value in LINQ
        // OrganizationId has HasConversion - must compare the value object directly, not .Value
        return await Context.Set<Vehicle>()
            .FirstOrDefaultAsync(v => 
                v.Plate.Value == plate.Value &&          
                v.OrganizationId == organizationId);      
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByPlateAndOrganizationIdAsync(LicensePlate plate, OrganizationId organizationId)
    {
        // Plate is an owned type (OwnsOne) - can use .Value in LINQ
        // OrganizationId has HasConversion - must compare the value object directly, not .Value
        return await Context.Set<Vehicle>()
            .AnyAsync(v => 
                v.Plate.Value == plate.Value &&          // Owned type → OK usar .Value
                v.OrganizationId == organizationId);      // ValueConverter → NO usar .Value
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Vehicle>> FindByOrganizationIdAsync(OrganizationId organizationId)
    {
        // OrganizationId has HasConversion - must compare the value object directly, not .Value
        return await Context.Set<Vehicle>()
            .Where(v => v.OrganizationId == organizationId)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Vehicle>> FindEnabledByOrganizationIdAsync(OrganizationId organizationId)
    {
        // OrganizationId has HasConversion - must compare the value object directly, not .Value
        return await Context.Set<Vehicle>()
            .Where(v => 
                v.OrganizationId == organizationId && 
                v.State == VehicleState.Enabled)
            .ToListAsync();
    }
}