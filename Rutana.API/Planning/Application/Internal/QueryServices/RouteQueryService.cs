using Rutana.API.Planning.Domain.Model.Queries;
using Rutana.API.Planning.Domain.Repositories;
using Rutana.API.Planning.Domain.Services;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using RouteDraftAggregate = Rutana.API.Planning.Domain.Model.Aggregates.RouteDraft;
using RouteAggregate = Rutana.API.Planning.Domain.Model.Aggregates.Route;

namespace Rutana.API.Planning.Application.Internal.QueryServices;

/// <summary>
/// Route query service implementation.
/// Handles all queries related to route and route draft retrieval.
/// </summary>
/// <param name="routeRepository">The route repository.</param>
/// <param name="routeDraftRepository">The route draft repository.</param>
public class RouteQueryService(
    IRouteRepository routeRepository,
    IRouteDraftRepository routeDraftRepository)
    : IRouteQueryService
{
    /// <inheritdoc />
    public async Task<RouteAggregate?> Handle(GetRouteByIdQuery query)
    {
        return await routeRepository.FindByIdAsync(query.RouteId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RouteAggregate>> Handle(GetRoutesByOrganizationIdQuery query)
    {
        var organizationId = new OrganizationId(query.OrganizationId);
        return await routeRepository.FindByOrganizationIdAsync(organizationId);
    }

    /// <inheritdoc />
    public async Task<RouteDraftAggregate?> Handle(GetRouteDraftByIdQuery query)
    {
        return await routeDraftRepository.FindByIdAsync(query.RouteDraftId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RouteDraftAggregate>> Handle(GetRouteDraftsByOrganizationIdQuery query)
    {
        var organizationId = new OrganizationId(query.OrganizationId);
        return await routeDraftRepository.FindByOrganizationIdAsync(organizationId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RouteDraftAggregate>> Handle(GetRouteDraftsByExecutionDateQuery query)
    {
        var organizationId = new OrganizationId(query.OrganizationId);
        return await routeDraftRepository.FindByExecutionDateAsync(organizationId, query.ExecutionDate);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RouteAggregate>> Handle(GetRoutesByExecutionDateQuery query)
    {
        var organizationId = new OrganizationId(query.OrganizationId);
        return await routeRepository.FindByExecutionDateAsync(organizationId, query.ExecutionDate);
    }
}