using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Interfaces.REST.Resources;

namespace Rutana.API.CRM.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert Location entity to LocationResource.
/// </summary>
public static class LocationResourceFromEntityAssembler
{
    /// <summary>
    /// Converts a Location entity to a LocationResource.
    /// </summary>
    /// <param name="location">The Location entity.</param>
    /// <returns>The LocationResource.</returns>
    public static LocationResource ToResourceFromEntity(Location location)
    {
        return new LocationResource(
            location.Id.Value,
            location.Latitude.Value,
            location.Longitude.Value,
            location.Address.Value,
            location.Proximity.ToString().ToLowerInvariant(),
            location.IsEnabled,
            location.ClientId.Value
        );
    }

    /// <summary>
    /// Converts a Location entity with its Client to a LocationResource.
    /// Note: This overload is kept for backward compatibility but returns the same structure.
    /// </summary>
    /// <param name="location">The Location entity.</param>
    /// <param name="client">The Client entity that owns the location (not used in current structure).</param>
    /// <returns>The LocationResource.</returns>
    public static LocationResource ToResourceFromEntity(Location location, Client client)
    {
        return ToResourceFromEntity(location);
    }
}