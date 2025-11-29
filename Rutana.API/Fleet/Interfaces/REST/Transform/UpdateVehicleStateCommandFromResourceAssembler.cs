using Rutana.API.Fleet.Domain.Model.Commands;
using Rutana.API.Fleet.Interfaces.REST.Resources;

namespace Rutana.API.Fleet.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert UpdateVehicleStateResource to UpdateVehicleStateCommand.
/// </summary>
public static class UpdateVehicleStateCommandFromResourceAssembler
{
    /// <summary>
    /// Convert resource to command.
    /// </summary>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <param name="resource">The update vehicle state resource.</param>
    /// <returns>The update vehicle state command.</returns>
    public static UpdateVehicleStateCommand ToCommandFromResource(int vehicleId, UpdateVehicleStateResource resource)
    {
        return new UpdateVehicleStateCommand(vehicleId, resource.State);
    }
}