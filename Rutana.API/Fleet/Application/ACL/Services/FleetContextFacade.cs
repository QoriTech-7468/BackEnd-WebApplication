using Rutana.API.Fleet.Domain.Repositories;
using Rutana.API.Fleet.Interfaces.ACL;

namespace Rutana.API.Fleet.Application.ACL.Services;

/// <summary>
/// Fleet Context Facade implementation.
/// Provides Fleet bounded context capabilities to external bounded contexts.
/// </summary>
/// <param name="vehicleRepository">The vehicle repository.</param>
public class FleetContextFacade(
    IVehicleRepository vehicleRepository) : IFleetContextFacade
{
    /// <inheritdoc />
    public async Task<bool> ExistsVehicleByIdAsync(int vehicleId)
    {
        var vehicle = await vehicleRepository.FindByIdAsync(vehicleId);
        return vehicle is not null;
    }

    /// <inheritdoc />
    public async Task<bool> IsVehicleEnabledAsync(int vehicleId)
    {
        var vehicle = await vehicleRepository.FindByIdAsync(vehicleId);
        return vehicle?.IsEnabled ?? false;
    }

    /// <inheritdoc />
    public async Task<decimal?> GetVehicleCapacityKgAsync(int vehicleId)
    {
        var vehicle = await vehicleRepository.FindByIdAsync(vehicleId);
        return vehicle?.Capacity.Value;
    }

    /// <inheritdoc />
    public async Task<int?> GetOrganizationIdByVehicleIdAsync(int vehicleId)
    {
        var vehicle = await vehicleRepository.FindByIdAsync(vehicleId);
        return vehicle?.OrganizationId.Value;
    }
}