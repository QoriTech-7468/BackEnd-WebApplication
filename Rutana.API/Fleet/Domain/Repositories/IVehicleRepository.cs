using Rutana.API.Fleet.Domain.Model.Aggregates;
using Rutana.API.Shared.Domain.Repositories;

namespace Rutana.API.Fleet.Domain.Repositories;

/// <summary>
/// Repository interface for managing Vehicle aggregate persistence.
/// </summary>
public interface IVehicleRepository : IBaseRepository<Vehicle>
{
    /// <summary>
    /// Finds a vehicle by its license plate within an organization.
    /// </summary>
    /// <param name="plate">The license plate to search for.</param>
    /// <param name="organizationId">The organization identifier.</param>
    /// <returns>The vehicle if found, otherwise null.</returns>
    Task<Vehicle?> FindByPlateAndOrganizationIdAsync(string plate, int organizationId);

    /// <summary>
    /// Checks if a vehicle with the given plate exists in the organization.
    /// </summary>
    /// <param name="plate">The license plate to check.</param>
    /// <param name="organizationId">The organization identifier.</param>
    /// <returns>True if exists, otherwise false.</returns>
    Task<bool> ExistsByPlateAndOrganizationIdAsync(string plate, int organizationId);

    /// <summary>
    /// Finds all vehicles belonging to an organization.
    /// </summary>
    /// <param name="organizationId">The organization identifier.</param>
    /// <returns>A collection of vehicles.</returns>
    Task<IEnumerable<Vehicle>> FindByOrganizationIdAsync(int organizationId);

    /// <summary>
    /// Finds all enabled vehicles belonging to an organization.
    /// </summary>
    /// <param name="organizationId">The organization identifier.</param>
    /// <returns>A collection of enabled vehicles.</returns>
    Task<IEnumerable<Vehicle>> FindEnabledByOrganizationIdAsync(int organizationId);
}