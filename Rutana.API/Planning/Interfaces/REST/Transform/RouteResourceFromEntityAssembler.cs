using Rutana.API.Planning.Interfaces.REST.Resources;
using RouteAggregate = Rutana.API.Planning.Domain.Model.Aggregates.Route;

namespace Rutana.API.Planning.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert Route entity to RouteResource.
/// </summary>
public static class RouteResourceFromEntityAssembler
{
    /// <summary>
    /// Converts a Route entity to a RouteResource.
    /// </summary>
    /// <param name="entity">The Route entity.</param>
    /// <returns>The RouteResource.</returns>
    public static RouteResource ToResourceFromEntity(RouteAggregate entity)
    {
        var deliveries = entity.Deliveries
            .Select(DeliveryResourceFromEntityAssembler.ToResourceFromEntity);

        var teamMembers = entity.TeamMembers
            .Select(RouteTeamMemberResourceFromEntityAssembler.ToResourceFromEntity);

        return new RouteResource(
            entity.Id,
            entity.OrganizationId.Value,
            entity.VehicleId.Value,
            entity.ColorCode.Value,
            entity.ExecutionDate,
            entity.StartedAt,
            entity.EndedAt,
            entity.Status.ToString(),
            deliveries,
            teamMembers);
    }
}