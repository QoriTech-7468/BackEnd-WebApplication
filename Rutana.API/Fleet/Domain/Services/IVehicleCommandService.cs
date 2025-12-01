using Rutana.API.Fleet.Domain.Model.Aggregates;
using Rutana.API.Fleet.Domain.Model.Commands;

namespace Rutana.API.Fleet.Domain.Services;

/// <summary>
/// Vehicle command service interface.
/// </summary>
public interface IVehicleCommandService
{
    /// <summary>
    /// Handle register vehicle command.
    /// </summary>
    /// <param name="command">The register vehicle command.</param>
    /// <returns>The created vehicle.</returns>
    Task<Vehicle?> Handle(RegisterVehicleCommand command);

    /// <summary>
    /// Handle update vehicle profile command.
    /// </summary>
    /// <param name="command">The update vehicle profile command.</param>
    /// <returns>The updated vehicle.</returns>
    Task<Vehicle?> Handle(UpdateVehicleProfileCommand command);

    /// <summary>
    /// Handle update vehicle state command.
    /// </summary>
    /// <param name="command">The update vehicle state command.</param>
    /// <returns>The updated vehicle.</returns>
    Task<Vehicle?> Handle(UpdateVehicleStateCommand command);
}