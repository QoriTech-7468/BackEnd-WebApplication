using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Interfaces.REST.Resources;

namespace Rutana.API.CRM.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert Client and Locations to ClientWithLocationsResource.
/// </summary>
public static class ClientWithLocationsResourceAssembler
{
    /// <summary>
    /// Converts a Client entity and its locations to a ClientWithLocationsResource.
    /// </summary>
    /// <param name="client">The Client entity.</param>
    /// <param name="locations">The collection of Location entities.</param>
    /// <returns>The ClientWithLocationsResource.</returns>
    public static ClientWithLocationsResource ToResourceFromEntity(Client client, IEnumerable<Location> locations)
    {
        var locationResources = locations.Select(location => 
            new LocationResource(
                location.Id.Value,
                location.Name.Value,
                location.Proximity.ToString(),
                location.IsEnabled,
                ClientSummaryResourceFromEntityAssembler.ToResourceFromEntity(client)
            ));

        return new ClientWithLocationsResource(
            client.Id.Value,
            client.CompanyName.Value,
            client.IsEnabled,
            locationResources);
    }
}