using Rutana.API.Planning.Domain.Model.Commands;
using Rutana.API.Planning.Interfaces.REST.Resources;

namespace Rutana.API.Planning.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert SaveRouteDraftChangesResource to SaveRouteDraftChangesCommand.
/// </summary>
public static class SaveRouteDraftChangesCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a SaveRouteDraftChangesResource to a SaveRouteDraftChangesCommand.
    /// </summary>
    /// <param name="routeDraftId">The route draft identifier.</param>
    /// <param name="resource">The SaveRouteDraftChangesResource.</param>
    /// <returns>The SaveRouteDraftChangesCommand.</returns>
    public static SaveRouteDraftChangesCommand ToCommandFromResource(int routeDraftId, SaveRouteDraftChangesResource resource)
    {
        return new SaveRouteDraftChangesCommand(
            routeDraftId,
            resource.LocationIds,
            resource.TeamMemberIds,
            resource.VehicleId,
            resource.ColorCode,
            resource.ExecutionDate,
            resource.StartedAt,
            resource.EndedAt);
    }
}