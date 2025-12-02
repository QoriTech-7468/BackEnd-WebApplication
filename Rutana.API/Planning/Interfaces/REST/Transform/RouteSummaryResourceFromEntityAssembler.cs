using Rutana.API.Planning.Interfaces.REST.Resources;
using RouteAggregate = Rutana.API.Planning.Domain.Model.Aggregates.Route;

namespace Rutana.API.Planning.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert Route entity to RouteSummaryResource.
/// </summary>
public static class RouteSummaryResourceFromEntityAssembler
{
    /// <summary>
    /// Converts a Route entity to a RouteSummaryResource.
    /// </summary>
    /// <param name="entity">The Route entity.</param>
    /// <returns>The RouteSummaryResource.</returns>
    public static RouteSummaryResource ToResourceFromEntity(RouteAggregate entity)
    {
        return new RouteSummaryResource(
            entity.Id,
            entity.ColorCode.Value,
            entity.StartedAt,
            entity.EndedAt,
            entity.Status.ToString());
    }
}

