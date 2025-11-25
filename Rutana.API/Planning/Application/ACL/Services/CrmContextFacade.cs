using Rutana.API.CRM.Domain.Model.Queries;
using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.CRM.Domain.Services;
using Rutana.API.Planning.Interfaces.ACL;

namespace Rutana.API.Planning.Application.ACL.Services;

/// <summary>
/// Facade for the CRM context.
/// </summary>
/// <param name="locationQueryService">
/// The location query service.
/// </param>
public class CrmContextFacade(ILocationQueryService locationQueryService) : ICrmContextFacade
{
    /// <inheritdoc />
    public async Task<bool> ExistsLocationByIdAsync(int locationId)
    {
        var locationIdVo = new LocationId(locationId);
        var query = new GetLocationByIdQuery(locationIdVo);
        var location = await locationQueryService.Handle(query);
        return location is not null;
    }

    /// <inheritdoc />
    public async Task<bool> IsLocationEnabledAsync(int locationId)
    {
        var locationIdVo = new LocationId(locationId);
        var query = new GetLocationByIdQuery(locationIdVo);
        var location = await locationQueryService.Handle(query);
        return location?.IsEnabled ?? false;
    }

    /// <inheritdoc />
    public async Task<int> FetchClientIdByLocationIdAsync(int locationId)
    {
        var locationIdVo = new LocationId(locationId);
        var query = new GetLocationByIdQuery(locationIdVo);
        var location = await locationQueryService.Handle(query);
        return location?.ClientId.Value ?? 0;
    }
}