using Rutana.API.Planning.Domain.Model.Commands;
using Rutana.API.Planning.Interfaces.REST.Resources;

namespace Rutana.API.Planning.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert CreateRouteDraftResource to CreateRouteDraftCommand.
/// </summary>
public static class CreateRouteDraftCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a CreateRouteDraftResource to a CreateRouteDraftCommand.
    /// </summary>
    /// <param name="resource">The CreateRouteDraftResource.</param>
    /// <returns>The CreateRouteDraftCommand.</returns>
    public static CreateRouteDraftCommand ToCommandFromResource(CreateRouteDraftResource resource)
    {
        return new CreateRouteDraftCommand(
            resource.OrganizationId,
            resource.ColorCode,
            resource.ExecutionDate);
    }
}