using Rutana.API.Fleet.Interfaces.ACL;
using Rutana.API.Planning.Application.Internal.OutboundServices;

namespace Rutana.API.Planning.Infrastructure.OutboundServices;

/// <summary>
/// Outbound service implementation for Fleet bounded context operations.
/// Wraps the Fleet Context Facade to provide a single point of contact for Planning.
/// </summary>
/// <param name="fleetContextFacade">The fleet context facade.</param>
public class FleetService(IFleetContextFacade fleetContextFacade) : IFleetService
{
    /// <inheritdoc />
    public async Task<bool> ExistsVehicleByIdAsync(int vehicleId)
    {
        return await fleetContextFacade.ExistsVehicleByIdAsync(vehicleId);
    }

    /// <inheritdoc />
    public async Task<bool> IsVehicleEnabledAsync(int vehicleId)
    {
        return await fleetContextFacade.IsVehicleEnabledAsync(vehicleId);
    }

    /// <inheritdoc />
    public async Task<decimal?> GetVehicleCapacityKgAsync(int vehicleId)
    {
        return await fleetContextFacade.GetVehicleCapacityKgAsync(vehicleId);
    }

    /// <inheritdoc />
    public async Task<int?> GetOrganizationIdByVehicleIdAsync(int vehicleId)
    {
        return await fleetContextFacade.GetOrganizationIdByVehicleIdAsync(vehicleId);
    }
}