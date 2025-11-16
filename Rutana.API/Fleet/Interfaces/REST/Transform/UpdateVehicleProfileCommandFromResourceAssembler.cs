using Rutana.API.Fleet.Domain.Model.Commands;
using Rutana.API.Fleet.Interfaces.REST.Resources;

namespace Rutana.API.Fleet.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert UpdateVehicleProfileResource to UpdateVehicleProfileCommand.
/// </summary>
public static class UpdateVehicleProfileCommandFromResourceAssembler
{
    /// <summary>
    /// Converts an UpdateVehicleProfileResource to an UpdateVehicleProfileCommand.
    /// </summary>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <param name="resource">The UpdateVehicleProfileResource.</param>
    /// <returns>The UpdateVehicleProfileCommand.</returns>
    public static UpdateVehicleProfileCommand ToCommandFromResource(int vehicleId, UpdateVehicleProfileResource resource)
    {
        return new UpdateVehicleProfileCommand(
            vehicleId,
            resource.Plate,
            resource.CapacityKg);
    }
}