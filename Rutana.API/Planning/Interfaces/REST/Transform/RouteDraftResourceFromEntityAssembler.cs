using Rutana.API.Planning.Interfaces.REST.Resources;
using RouteDraftAggregate = Rutana.API.Planning.Domain.Model.Aggregates.RouteDraft;

namespace Rutana.API.Planning.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert RouteDraft entity to RouteDraftResource.
/// </summary>
public static class RouteDraftResourceFromEntityAssembler
{
    /// <summary>
    /// Converts a RouteDraft entity to a RouteDraftResource.
    /// </summary>
    /// <param name="entity">The RouteDraft entity.</param>
    /// <returns>The RouteDraftResource.</returns>
    public static RouteDraftResource ToResourceFromEntity(RouteDraftAggregate entity)
    {
        var deliveries = entity.Deliveries
            .Select(DeliveryResourceFromEntityAssembler.ToResourceFromEntity);

        var teamMembers = entity.TeamMembers
            .Select(RouteTeamMemberResourceFromEntityAssembler.ToResourceFromEntity);

        return new RouteDraftResource(
            entity.Id,
            entity.OrganizationId.Value,
            entity.VehicleId?.Value,
            entity.ColorCode.Value,
            entity.StartedAt,
            entity.EndedAt,
            deliveries,
            teamMembers);
    }
}