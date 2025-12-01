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
        // Create a ClientSummaryResource with only the ClientId
        var clientSummary = new ClientSummaryResource(
            location.ClientId.Value,
            string.Empty // We don't have client name here
        );

        return new LocationResource(
            location.Id.Value,
            location.Name.Value, //  Es Name, no LocationName
            location.Proximity.ToString(),
            location.IsEnabled,
            clientSummary
        );
    }

    /// <summary>
    /// Converts a Location entity with its Client to a LocationResource.
    /// </summary>
    /// <param name="location">The Location entity.</param>
    /// <param name="client">The Client entity that owns the location.</param>
    /// <returns>The LocationResource.</returns>
    public static LocationResource ToResourceFromEntity(Location location, Client client)
    {
        return new LocationResource(
            location.Id.Value,
            location.Name.Value, //  Es Name, no LocationName
            location.Proximity.ToString(),
            location.IsEnabled,
            ClientSummaryResourceFromEntityAssembler.ToResourceFromEntity(client)
        );
    }
}