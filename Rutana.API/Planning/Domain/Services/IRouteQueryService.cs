using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.Planning.Domain.Model.Queries;
using RouteDraftAggregate = Rutana.API.Planning.Domain.Model.Aggregates.RouteDraft;
using RouteAggregate = Rutana.API.Planning.Domain.Model.Aggregates.Route;

namespace Rutana.API.Planning.Domain.Services;

//ROUTE AGGREGATE - alias para evitar ambigüedades con otras clases llamadas Route de Microsoft.EntityFramework u otras librerías.

/// <summary>
/// Represents the route query service in the Rutana Planning System.
/// </summary>
public interface IRouteQueryService
{
    /// <summary>
    /// Handles the get route by id query.
    /// </summary>
    /// <param name="query">The <see cref="GetRouteByIdQuery"/> query to handle.</param>
    /// <returns>The <see cref="RouteAggregate"/> if found, otherwise null.</returns>
    Task<RouteAggregate?> Handle(GetRouteByIdQuery query);

    /// <summary>
    /// Handles the get routes by organization id query.
    /// </summary>
    /// <param name="query">The <see cref="GetRoutesByOrganizationIdQuery"/> query to handle.</param>
    /// <returns>A collection of all routes belonging to the organization.</returns>
    Task<IEnumerable<RouteAggregate>> Handle(GetRoutesByOrganizationIdQuery query);

    /// <summary>
    /// Handles the get route draft by id query.
    /// </summary>
    /// <param name="query">The <see cref="GetRouteDraftByIdQuery"/> query to handle.</param>
    /// <returns>The <see cref="RouteDraftAggregate"/> if found, otherwise null.</returns>
    Task<RouteDraftAggregate?> Handle(GetRouteDraftByIdQuery query);

    /// <summary>
    /// Handles the get route drafts by organization id query.
    /// </summary>
    /// <param name="query">The <see cref="GetRouteDraftsByOrganizationIdQuery"/> query to handle.</param>
    /// <returns>A collection of all route drafts belonging to the organization.</returns>
    Task<IEnumerable<RouteDraftAggregate>> Handle(GetRouteDraftsByOrganizationIdQuery query);

    /// <summary>
    /// Handles the get route drafts by execution date query.
    /// </summary>
    /// <param name="query">The <see cref="GetRouteDraftsByExecutionDateQuery"/> query to handle.</param>
    /// <returns>A collection of route drafts for the specified execution date.</returns>
    Task<IEnumerable<RouteDraftAggregate>> Handle(GetRouteDraftsByExecutionDateQuery query);

    /// <summary>
    /// Handles the get routes by execution date query.
    /// </summary>
    /// <param name="query">The <see cref="GetRoutesByExecutionDateQuery"/> query to handle.</param>
    /// <returns>A collection of routes for the specified execution date.</returns>
    Task<IEnumerable<RouteAggregate>> Handle(GetRoutesByExecutionDateQuery query);

    /// <summary>
    /// Handles the get available locations for deliveries query.
    /// </summary>
    /// <param name="query">The <see cref="GetAvailableLocationsForDeliveriesQuery"/> query to handle.</param>
    /// <returns>A collection of available locations that can be used to create deliveries.</returns>
    Task<IEnumerable<Location>> Handle(GetAvailableLocationsForDeliveriesQuery query);
}