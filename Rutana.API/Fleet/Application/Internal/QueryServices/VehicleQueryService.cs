using Rutana.API.Fleet.Domain.Model.Aggregates;
using Rutana.API.Fleet.Domain.Model.Queries;
using Rutana.API.Fleet.Domain.Repositories;
using Rutana.API.Fleet.Domain.Services;

namespace Rutana.API.Fleet.Application.Internal.QueryServices;

/// <summary>
/// Vehicle query service implementation.
/// Handles all queries related to vehicle retrieval.
/// </summary>
/// <param name="vehicleRepository">The vehicle repository.</param>
public class VehicleQueryService(IVehicleRepository vehicleRepository)
    : IVehicleQueryService
{
    /// <inheritdoc />
    public async Task<Vehicle?> Handle(GetVehicleByIdQuery query)
    {
        return await vehicleRepository.FindByIdAsync(query.VehicleId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Vehicle>> Handle(GetVehiclesByOrganizationIdQuery query)
    {
        return await vehicleRepository.FindByOrganizationIdAsync(query.OrganizationId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Vehicle>> Handle(GetEnabledVehiclesByOrganizationIdQuery query)
    {
        return await vehicleRepository.FindEnabledByOrganizationIdAsync(query.OrganizationId);
    }
}