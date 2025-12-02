using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Interfaces.ACL;
using Rutana.API.Planning.Application.Internal.OutboundServices;

namespace Rutana.API.Planning.Infrastructure.OutboundServices;

/// <summary>
/// Outbound service implementation for CRM bounded context operations.
/// Wraps the CRM Context Facade to provide a single point of contact for Planning.
/// </summary>
/// <param name="crmContextFacade">The CRM context facade.</param>
public class CrmService(ICrmContextFacade crmContextFacade) : ICrmService
{
    /// <inheritdoc />
    public async Task<bool> ExistsLocationByIdAsync(int locationId)
    {
        return await crmContextFacade.ExistsLocationByIdAsync(locationId);
    }

    /// <inheritdoc />
    public async Task<bool> IsLocationEnabledAsync(int locationId)
    {
        return await crmContextFacade.IsLocationEnabledAsync(locationId);
    }

    /// <inheritdoc />
    public async Task<int?> GetClientIdByLocationIdAsync(int locationId)
    {
        return await crmContextFacade.GetClientIdByLocationIdAsync(locationId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Location>> GetLocationsByOrganizationIdAsync(int organizationId, bool onlyEnabled = true)
    {
        return await crmContextFacade.GetLocationsByOrganizationIdAsync(organizationId, onlyEnabled);
    }
}