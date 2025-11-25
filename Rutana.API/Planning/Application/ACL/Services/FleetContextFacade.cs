using Rutana.API.Fleet.Domain.Model.Queries;
using Rutana.API.Fleet.Domain.Model.ValueObjects;
using Rutana.API.Fleet.Domain.Services;
using Rutana.API.Planning.Interfaces.ACL;

namespace Rutana.API.Planning.Application.ACL.Services;

/// <summary>
/// Facade for the Fleet context.
/// </summary>
/// <param name="vehicleQueryService">
/// The vehicle query service.
/// </param>
public class FleetContextFacade(IVehicleQueryService vehicleQueryService) : IFleetContextFacade
{
    /// <inheritdoc />
    public async Task<bool> ExistsVehicleByIdAsync(int vehicleId)
    {
        var query = new GetVehicleByIdQuery(vehicleId);
        var vehicle = await vehicleQueryService.Handle(query);
        return vehicle is not null;
    }

    /// <inheritdoc />
    public async Task<bool> IsVehicleEnabledAsync(int vehicleId)
    {
        var query = new GetVehicleByIdQuery(vehicleId);
        var vehicle = await vehicleQueryService.Handle(query);
        return vehicle?.State == VehicleState.Enabled;
    }

    /// <inheritdoc />
    public async Task<decimal?> FetchVehicleCapacityAsync(int vehicleId)
    {
        var query = new GetVehicleByIdQuery(vehicleId);
        var vehicle = await vehicleQueryService.Handle(query);
        return vehicle?.Capacity.Value;
    }
}