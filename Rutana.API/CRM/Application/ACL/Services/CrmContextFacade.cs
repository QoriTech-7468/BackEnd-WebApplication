using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Domain.Repositories;
using Rutana.API.CRM.Interfaces.ACL;
using Rutana.API.Shared.Domain.Repositories;

namespace Rutana.API.CRM.Application.ACL.Services;

/// <summary>
/// CRM Context Facade implementation.
/// Provides CRM bounded context capabilities to external bounded contexts.
/// </summary>
/// <param name="clientRepository">The client repository.</param>
/// <param name="locationRepository">The location repository.</param>
public class CrmContextFacade(
    IClientRepository clientRepository,
    ILocationRepository locationRepository) : ICrmContextFacade
{
    /// <inheritdoc />
    public async Task<bool> ExistsClientByIdAsync(int clientId)
    {
        var client = await clientRepository.FindByIdAsync(clientId);
        return client is not null;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsLocationByIdAsync(int locationId)
    {
        var location = await locationRepository.FindByIdAsync(locationId);
        return location is not null;
    }

    /// <inheritdoc />
    public async Task<int?> GetClientIdByLocationIdAsync(int locationId)
    {
        var location = await locationRepository.FindByIdAsync(locationId);
        return location?.ClientId.Value;
    }

    /// <inheritdoc />
    public async Task<bool> IsLocationEnabledAsync(int locationId)
    {
        var location = await locationRepository.FindByIdAsync(locationId);
        return location?.IsEnabled ?? false;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Location>> GetLocationsByOrganizationIdAsync(int organizationId, bool onlyEnabled = true)
    {
        // Get all clients for the organization
        var clients = await clientRepository.FindByOrganizationIdAsync(organizationId);
        
        // Get all locations for each client
        var allLocations = new List<Location>();
        foreach (var client in clients)
        {
            var clientLocations = await locationRepository.FindByClientIdAsync(client.Id);
            allLocations.AddRange(clientLocations);
        }
        
        // Filter by enabled status if requested
        if (onlyEnabled)
        {
            return allLocations.Where(l => l.IsEnabled);
        }
        
        return allLocations;
    }
}