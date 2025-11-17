using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Domain.Model.Queries;
using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.CRM.Domain.Repositories;
using Rutana.API.CRM.Domain.Services;

namespace Rutana.API.CRM.Application.Internal.QueryServices;

/// <summary>
/// Location query service implementation.
/// </summary>
/// <param name="locationRepository">The location repository.</param>
public class LocationQueryService(ILocationRepository locationRepository) : ILocationQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Location>> Handle(GetLocationsByClientIdQuery query)
    {
        return await locationRepository.FindByClientIdAsync(query.ClientId.Value);
    }

    /// <inheritdoc />
    public async Task<Location?> Handle(GetLocationByIdQuery query)
    {
        return await locationRepository.FindByIdAsync(query.LocationId);
    }
}