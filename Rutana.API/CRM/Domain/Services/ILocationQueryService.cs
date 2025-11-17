using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Domain.Model.Queries;

namespace Rutana.API.CRM.Domain.Services;

/// <summary>
/// Represents the location query service in the Rutana CRM System.
/// </summary>
public interface ILocationQueryService
{
    /// <summary>
    /// Handles the get locations by client id query.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>A collection of locations.</returns>
    Task<IEnumerable<Location>> Handle(GetLocationsByClientIdQuery query);

    /// <summary>
    /// Handles the get location by id query.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>The location if found, otherwise null.</returns>
    Task<Location?> Handle(GetLocationByIdQuery query);
}