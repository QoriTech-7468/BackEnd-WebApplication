using Rutana.API.Fleet.Domain.Model.Commands;
using Rutana.API.Fleet.Interfaces.REST.Resources;

namespace Rutana.API.Fleet.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert RegisterVehicleResource to RegisterVehicleCommand.
/// </summary>
public static class RegisterVehicleCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a RegisterVehicleResource to a RegisterVehicleCommand.
    /// </summary>
    /// <param name="resource">The RegisterVehicleResource.</param>
    /// <returns>The RegisterVehicleCommand.</returns>
    public static RegisterVehicleCommand ToCommandFromResource(RegisterVehicleResource resource)
    {
        return new RegisterVehicleCommand(
            resource.OrganizationId,
            resource.Plate,
            resource.CapacityKg);
    }
}