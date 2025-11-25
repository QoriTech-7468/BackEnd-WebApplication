using Rutana.API.Fleet.Domain.Model.Aggregates;
using Rutana.API.Fleet.Interfaces.REST.Resources;

namespace Rutana.API.Fleet.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert Vehicle entity to VehicleResource.
/// </summary>
public static class VehicleResourceFromEntityAssembler
{
    /// <summary>
    /// Converts a Vehicle entity to a VehicleResource.
    /// </summary>
    /// <param name="entity">The Vehicle entity.</param>
    /// <returns>The VehicleResource.</returns>
    public static VehicleResource ToResourceFromEntity(Vehicle entity)
    {
        return new VehicleResource(
            entity.Id,
            entity.OrganizationId.Value,
            entity.Plate.Value,
            entity.Capacity.Value,
            entity.State.ToString());
    }
}