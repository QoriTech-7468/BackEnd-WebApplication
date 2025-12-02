using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.Planning.Interfaces.REST.Resources;

namespace Rutana.API.Planning.Interfaces.REST.Transform;

/// <summary>
/// Assembler for converting Location entity to AvailableLocationResource.
/// </summary>
public static class AvailableLocationResourceFromEntityAssembler
{
    /// <summary>
    /// Converts a Location entity to an AvailableLocationResource.
    /// </summary>
    /// <param name="location">The location entity.</param>
    /// <returns>The available location resource.</returns>
    public static AvailableLocationResource ToResourceFromEntity(Location location)
    {
        return new AvailableLocationResource(
            location.Id.Value,
            location.Address.Value,
            location.Latitude.Value,
            location.Longitude.Value);
    }
}

