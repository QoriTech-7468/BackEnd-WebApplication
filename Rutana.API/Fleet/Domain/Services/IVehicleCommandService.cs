using Rutana.API.Fleet.Domain.Model.Aggregates;
using Rutana.API.Fleet.Domain.Model.Commands;

namespace Rutana.API.Fleet.Domain.Services;

/// <summary>
/// Represents the vehicle command service in the Rutana Fleet Management System.
/// </summary>
public interface IVehicleCommandService
{
    /// <summary>
    /// Handles the register vehicle command.
    /// </summary>
    /// <param name="command">The <see cref="RegisterVehicleCommand"/> command to handle.</param>
    /// <returns>The registered <see cref="Vehicle"/> entity, or null if registration failed.</returns>
    Task<Vehicle?> Handle(RegisterVehicleCommand command);

    /// <summary>
    /// Handles the enable vehicle command.
    /// </summary>
    /// <param name="command">The <see cref="EnableVehicleCommand"/> command to handle.</param>
    /// <returns>The enabled <see cref="Vehicle"/> entity, or null if not found.</returns>
    Task<Vehicle?> Handle(EnableVehicleCommand command);

    /// <summary>
    /// Handles the disable vehicle command.
    /// </summary>
    /// <param name="command">The <see cref="DisableVehicleCommand"/> command to handle.</param>
    /// <returns>The disabled <see cref="Vehicle"/> entity, or null if not found.</returns>
    Task<Vehicle?> Handle(DisableVehicleCommand command);

    /// <summary>
    /// Handles the update vehicle profile command.
    /// </summary>
    /// <param name="command">The <see cref="UpdateVehicleProfileCommand"/> command to handle.</param>
    /// <returns>The updated <see cref="Vehicle"/> entity, or null if not found.</returns>
    Task<Vehicle?> Handle(UpdateVehicleProfileCommand command);
}