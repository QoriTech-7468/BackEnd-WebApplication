using Rutana.API.Planning.Interfaces.REST.Resources;
using RouteDraftAggregate = Rutana.API.Planning.Domain.Model.Aggregates.RouteDraft;

namespace Rutana.API.Planning.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert RouteDraft entity to RouteDraftSummaryResource.
/// </summary>
public static class RouteDraftSummaryResourceFromEntityAssembler
{
    /// <summary>
    /// Converts a RouteDraft entity to a RouteDraftSummaryResource.
    /// </summary>
    /// <param name="entity">The RouteDraft entity.</param>
    /// <returns>The RouteDraftSummaryResource.</returns>
    public static RouteDraftSummaryResource ToResourceFromEntity(RouteDraftAggregate entity)
    {
        return new RouteDraftSummaryResource(
            entity.Id,
            entity.ColorCode.Value);
    }
}

