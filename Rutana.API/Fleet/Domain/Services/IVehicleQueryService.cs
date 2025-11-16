using Rutana.API.Fleet.Domain.Model.Aggregates;
using Rutana.API.Fleet.Domain.Model.Queries;

namespace Rutana.API.Fleet.Domain.Services;

/// <summary>
/// Represents the vehicle query service in the Rutana Fleet Management System.
/// </summary>
public interface IVehicleQueryService
{
    /// <summary>
    /// Handles the get vehicle by id query.
    /// </summary>
    /// <param name="query">The <see cref="GetVehicleByIdQuery"/> query to handle.</param>
    /// <returns>The <see cref="Vehicle"/> if found, otherwise null.</returns>
    Task<Vehicle?> Handle(GetVehicleByIdQuery query);

    /// <summary>
    /// Handles the get vehicles by organization id query.
    /// </summary>
    /// <param name="query">The <see cref="GetVehiclesByOrganizationIdQuery"/> query to handle.</param>
    /// <returns>A collection of all vehicles belonging to the organization.</returns>
    Task<IEnumerable<Vehicle>> Handle(GetVehiclesByOrganizationIdQuery query);

    /// <summary>
    /// Handles the get enabled vehicles by organization id query.
    /// </summary>
    /// <param name="query">The <see cref="GetEnabledVehiclesByOrganizationIdQuery"/> query to handle.</param>
    /// <returns>A collection of all enabled vehicles belonging to the organization.</returns>
    Task<IEnumerable<Vehicle>> Handle(GetEnabledVehiclesByOrganizationIdQuery query);
}