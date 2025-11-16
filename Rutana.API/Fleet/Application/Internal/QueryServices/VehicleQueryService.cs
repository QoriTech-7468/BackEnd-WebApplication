using Rutana.API.Fleet.Domain.Model.Aggregates;
using Rutana.API.Fleet.Domain.Model.Queries;
using Rutana.API.Fleet.Domain.Repositories;
using Rutana.API.Fleet.Domain.Services;
using Rutana.API.Shared.Domain.Model.ValueObjects;

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
        var organizationId = new OrganizationId(query.OrganizationId);
        return await vehicleRepository.FindByOrganizationIdAsync(organizationId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Vehicle>> Handle(GetEnabledVehiclesByOrganizationIdQuery query)
    {
        var organizationId = new OrganizationId(query.OrganizationId);
        return await vehicleRepository.FindEnabledByOrganizationIdAsync(organizationId);
    }
}