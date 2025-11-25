using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.Fleet.Domain.Model.ValueObjects;
using Rutana.API.Planning.Domain.Model.Commands;
using Rutana.API.Planning.Domain.Model.ValueObjects;
using Rutana.API.Planning.Domain.Repositories;
using Rutana.API.Planning.Domain.Services;
using Rutana.API.Shared.Domain.Repositories;
using RouteDraftAggregate = Rutana.API.Planning.Domain.Model.Aggregates.RouteDraft;
using RouteAggregate = Rutana.API.Planning.Domain.Model.Aggregates.Route;

namespace Rutana.API.Planning.Application.Internal.CommandServices;

/// <summary>
/// Route command service implementation.
/// Handles all commands related to route and route draft management.
/// </summary>
/// <param name="routeDraftRepository">The route draft repository.</param>
/// <param name="routeRepository">The route repository.</param>
/// <param name="unitOfWork">The unit of work for transaction management.</param>
public class RouteCommandService(
    IRouteDraftRepository routeDraftRepository,
    IRouteRepository routeRepository,
    IUnitOfWork unitOfWork)
    : IRouteCommandService
{
    /// <inheritdoc />
    public async Task<RouteDraftAggregate?> Handle(CreateRouteDraftCommand command)
    {
        // Create the new route draft
        var routeDraft = RouteDraftAggregate.Create(command);
        
        // Add to repository
        await routeDraftRepository.AddAsync(routeDraft);
        
        // Save changes
        await unitOfWork.CompleteAsync();

        return routeDraft;
    }

    /// <inheritdoc />
    public async Task<RouteDraftAggregate?> Handle(SaveRouteDraftChangesCommand command)
    {
        // Find the route draft
        var routeDraft = await routeDraftRepository.FindByIdAsync(command.RouteDraftId);
        
        if (routeDraft == null)
            return null;

        // Apply changes to the draft
        routeDraft.ApplyChanges(command);
        
        // Update repository
        routeDraftRepository.Update(routeDraft);
        
        // Save changes
        await unitOfWork.CompleteAsync();

        return routeDraft;
    }

    /// <inheritdoc />
    public async Task<RouteAggregate?> Handle(PublishRouteCommand command)
    {
        // Find the route draft
        var routeDraft = await routeDraftRepository.FindByIdAsync(command.RouteDraftId);
        
        if (routeDraft == null)
            return null;

        try
        {
            // Create route from draft (this validates the draft)
            var route = RouteAggregate.FromDraft(routeDraft);
            
            // Add route to repository
            await routeRepository.AddAsync(route);
            
            // Remove the draft (it's now published)
            routeDraftRepository.Remove(routeDraft);
            
            // Save changes
            await unitOfWork.CompleteAsync();

            return route;
        }
        catch (InvalidOperationException)
        {
            // Validation failed
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<bool> Handle(DeleteRouteDraftCommand command)
    {
        // Find the route draft
        var routeDraft = await routeDraftRepository.FindByIdAsync(command.RouteDraftId);
        
        if (routeDraft == null)
            return false;

        // Remove from repository
        routeDraftRepository.Remove(routeDraft);
        
        // Save changes
        await unitOfWork.CompleteAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<RouteDraftAggregate?> Handle(AddLocationToRouteCommand command)
    {
        // Find the route draft
        var routeDraft = await routeDraftRepository.FindByIdAsync(command.RouteDraftId);
        
        if (routeDraft == null)
            return null;

        try
        {
            // Add location to the draft
            var locationId = new LocationId(command.LocationId);
            routeDraft.AddLocation(locationId);
            
            // Update repository
            routeDraftRepository.Update(routeDraft);
            
            // Save changes
            await unitOfWork.CompleteAsync();

            return routeDraft;
        }
        catch (InvalidOperationException)
        {
            // Location already exists in route
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<RouteDraftAggregate?> Handle(AssignMemberToVehicleTeamCommand command)
    {
        // Find the route draft
        var routeDraft = await routeDraftRepository.FindByIdAsync(command.RouteDraftId);
        
        if (routeDraft == null)
            return null;

        try
        {
            // Assign member to the draft
            var userId = new UserId(command.UserId);
            routeDraft.AssignMember(userId);
            
            // Update repository
            routeDraftRepository.Update(routeDraft);
            
            // Save changes
            await unitOfWork.CompleteAsync();

            return routeDraft;
        }
        catch (InvalidOperationException)
        {
            // User already assigned to route
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<RouteDraftAggregate?> Handle(AssignVehicleToRouteCommand command)
    {
        // Find the route draft
        var routeDraft = await routeDraftRepository.FindByIdAsync(command.RouteDraftId);
        
        if (routeDraft == null)
            return null;

        // Assign vehicle to the draft
        var vehicleId = new VehicleId(command.VehicleId);
        routeDraft.AssignVehicle(vehicleId);
        
        // Update repository
        routeDraftRepository.Update(routeDraft);
        
        // Save changes
        await unitOfWork.CompleteAsync();

        return routeDraft;
    }
}