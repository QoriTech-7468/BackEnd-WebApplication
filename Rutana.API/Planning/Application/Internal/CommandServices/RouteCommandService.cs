using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.Fleet.Domain.Model.ValueObjects;
using Rutana.API.Planning.Application.Internal.OutboundServices;
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
/// <param name="fleetService">The fleet outbound service for vehicle validation.</param>
/// <param name="crmService">The CRM outbound service for location validation.</param>
/// <param name="unitOfWork">The unit of work for transaction management.</param>
public class RouteCommandService(
    IRouteDraftRepository routeDraftRepository,
    IRouteRepository routeRepository,
    IFleetService fleetService,
    ICrmService crmService,
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

        // Validate vehicle if provided
        if (command.VehicleId.HasValue)
        {
            var vehicleExists = await fleetService.ExistsVehicleByIdAsync(command.VehicleId.Value);
            if (!vehicleExists)
                throw new InvalidOperationException($"Vehicle with id {command.VehicleId.Value} does not exist.");

            var vehicleIsEnabled = await fleetService.IsVehicleEnabledAsync(command.VehicleId.Value);
            if (!vehicleIsEnabled)
                throw new InvalidOperationException($"Vehicle with id {command.VehicleId.Value} is not enabled.");
        }

        // Validate locations if provided
        if (command.LocationIds != null && command.LocationIds.Any())
        {
            foreach (var locationId in command.LocationIds)
            {
                var locationExists = await crmService.ExistsLocationByIdAsync(locationId);
                if (!locationExists)
                    throw new InvalidOperationException($"Location with id {locationId} does not exist.");

                var locationIsEnabled = await crmService.IsLocationEnabledAsync(locationId);
                if (!locationIsEnabled)
                    throw new InvalidOperationException($"Location with id {locationId} is not enabled.");
            }
        }

        // TODO: Validate team members when IAM context is ready
        // if (command.TeamMemberIds != null && command.TeamMemberIds.Any())
        // {
        //     foreach (var userId in command.TeamMemberIds)
        //     {
        //         var userExists = await iamService.ExistsUserByIdAsync(userId);
        //         if (!userExists)
        //             throw new InvalidOperationException($"User with id {userId} does not exist.");
        //     }
        // }

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
            // Validate vehicle before publishing
            if (routeDraft.VehicleId != null)
            {
                var vehicleExists = await fleetService.ExistsVehicleByIdAsync(routeDraft.VehicleId.Value);
                if (!vehicleExists)
                    throw new InvalidOperationException($"Cannot publish route: vehicle with id {routeDraft.VehicleId.Value} does not exist.");

                var vehicleIsEnabled = await fleetService.IsVehicleEnabledAsync(routeDraft.VehicleId.Value);
                if (!vehicleIsEnabled)
                    throw new InvalidOperationException($"Cannot publish route: vehicle with id {routeDraft.VehicleId.Value} is not enabled.");
            }

            // Validate all locations before publishing
            foreach (var delivery in routeDraft.Deliveries)
            {
                var locationExists = await crmService.ExistsLocationByIdAsync(delivery.LocationId.Value);
                if (!locationExists)
                    throw new InvalidOperationException($"Cannot publish route: location with id {delivery.LocationId.Value} does not exist.");

                var locationIsEnabled = await crmService.IsLocationEnabledAsync(delivery.LocationId.Value);
                if (!locationIsEnabled)
                    throw new InvalidOperationException($"Cannot publish route: location with id {delivery.LocationId.Value} is not enabled.");
            }

            // TODO: Validate all team members when IAM context is ready
            // foreach (var member in routeDraft.TeamMembers)
            // {
            //     var userExists = await iamService.ExistsUserByIdAsync(member.UserId.Value);
            //     if (!userExists)
            //         throw new InvalidOperationException($"Cannot publish route: user with id {member.UserId.Value} does not exist.");
            // }

            // Create route from draft (this validates the draft structure)
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

        // Validate location exists
        var locationExists = await crmService.ExistsLocationByIdAsync(command.LocationId);
        if (!locationExists)
            throw new InvalidOperationException($"Location with id {command.LocationId} does not exist.");

        // Validate location is enabled
        var locationIsEnabled = await crmService.IsLocationEnabledAsync(command.LocationId);
        if (!locationIsEnabled)
            throw new InvalidOperationException($"Location with id {command.LocationId} is not enabled.");

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

        // TODO: Validate user when IAM context is ready
        // var userExists = await iamService.ExistsUserByIdAsync(command.UserId);
        // if (!userExists)
        //     throw new InvalidOperationException($"User with id {command.UserId} does not exist.");

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

        // Validate vehicle exists
        var vehicleExists = await fleetService.ExistsVehicleByIdAsync(command.VehicleId);
        if (!vehicleExists)
            throw new InvalidOperationException($"Vehicle with id {command.VehicleId} does not exist.");

        // Validate vehicle is enabled
        var vehicleIsEnabled = await fleetService.IsVehicleEnabledAsync(command.VehicleId);
        if (!vehicleIsEnabled)
            throw new InvalidOperationException($"Vehicle with id {command.VehicleId} is not enabled.");

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