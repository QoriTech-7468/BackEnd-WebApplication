using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.Planning.Application.Internal.OutboundServices;
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
/// <param name="crmService">The CRM service for accessing location data.</param>
public class RouteQueryService(
    IRouteRepository routeRepository,
    IRouteDraftRepository routeDraftRepository,
    ICrmService crmService)
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

    /// <inheritdoc />
    public async Task<IEnumerable<Location>> Handle(GetAvailableLocationsForDeliveriesQuery query)
    {
        // 1. Get all enabled locations for the organization
        var allLocations = await crmService.GetLocationsByOrganizationIdAsync(query.OrganizationId, onlyEnabled: true);

        // 2. Get all used LocationIds from deliveries with the specified execution date (optimized query)
        // Execute sequentially to avoid DbContext concurrency issues (both services use the same AppDbContext)
        var organizationId = new OrganizationId(query.OrganizationId);
        var usedLocationIds = (await routeRepository.GetUsedLocationIdsByExecutionDateAsync(organizationId, query.ExecutionDate)).ToHashSet();

        // 3. Filter locations that are NOT in the used LocationIds set
        var availableLocations = allLocations
            .Where(location => !usedLocationIds.Contains(location.Id.Value))
            .ToList();

        return availableLocations;
    }
}