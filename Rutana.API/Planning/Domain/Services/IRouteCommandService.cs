using Rutana.API.Planning.Domain.Model.Commands;
using RouteDraftAggregate = Rutana.API.Planning.Domain.Model.Aggregates.RouteDraft;
using RouteAggregate = Rutana.API.Planning.Domain.Model.Aggregates.Route;

namespace Rutana.API.Planning.Domain.Services;

//ROUTE AGGREGATE - alias para evitar ambigüedades con otras clases llamadas Route de Microsoft.EntityFramework u otras librerías.

/// <summary>
/// Represents the route command service in the Rutana Planning System.
/// </summary>
public interface IRouteCommandService
{
    /// <summary>
    /// Handles the create route draft command.
    /// </summary>
    /// <param name="command">The <see cref="CreateRouteDraftCommand"/> command to handle.</param>
    /// <returns>The created <see cref="RouteDraftAggregate"/> entity, or null if creation failed.</returns>
    Task<RouteDraftAggregate?> Handle(CreateRouteDraftCommand command);

    /// <summary>
    /// Handles the save route draft changes command.
    /// </summary>
    /// <param name="command">The <see cref="SaveRouteDraftChangesCommand"/> command to handle.</param>
    /// <returns>The updated <see cref="RouteDraftAggregate"/> entity, or null if not found.</returns>
    Task<RouteDraftAggregate?> Handle(SaveRouteDraftChangesCommand command);

    /// <summary>
    /// Handles the publish route command.
    /// </summary>
    /// <param name="command">The <see cref="PublishRouteCommand"/> command to handle.</param>
    /// <returns>The published <see cref="RouteAggregate"/> entity, or null if publication failed.</returns>
    Task<RouteAggregate?> Handle(PublishRouteCommand command);

    /// <summary>
    /// Handles the delete route draft command.
    /// </summary>
    /// <param name="command">The <see cref="DeleteRouteDraftCommand"/> command to handle.</param>
    /// <returns>True if deletion was successful, otherwise false.</returns>
    Task<bool> Handle(DeleteRouteDraftCommand command);

    /// <summary>
    /// Handles the add location to route command.
    /// </summary>
    /// <param name="command">The <see cref="AddLocationToRouteCommand"/> command to handle.</param>
    /// <returns>The updated <see cref="RouteDraftAggregate"/> entity, or null if not found.</returns>
    Task<RouteDraftAggregate?> Handle(AddLocationToRouteCommand command);

    /// <summary>
    /// Handles the assign member to vehicle team command.
    /// </summary>
    /// <param name="command">The <see cref="AssignMemberToVehicleTeamCommand"/> command to handle.</param>
    /// <returns>The updated <see cref="RouteDraftAggregate"/> entity, or null if not found.</returns>
    Task<RouteDraftAggregate?> Handle(AssignMemberToVehicleTeamCommand command);

    /// <summary>
    /// Handles the assign vehicle to route command.
    /// </summary>
    /// <param name="command">The <see cref="AssignVehicleToRouteCommand"/> command to handle.</param>
    /// <returns>The updated <see cref="RouteDraftAggregate"/> entity, or null if not found.</returns>
    Task<RouteDraftAggregate?> Handle(AssignVehicleToRouteCommand command);
}