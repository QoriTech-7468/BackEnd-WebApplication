using Rutana.API.CRM.Domain.Model.Commands;
using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.CRM.Interfaces.REST.Resources;

namespace Rutana.API.CRM.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert UpdateLocationStateResource to UpdateLocationStateCommand.
/// </summary>
public static class UpdateLocationStateCommandFromResourceAssembler
{
    /// <summary>
    /// Convert resource to command.
    /// </summary>
    /// <param name="locationId">The location identifier.</param>
    /// <param name="resource">The update location state resource.</param>
    /// <returns>The update location state command.</returns>
    public static UpdateLocationStateCommand ToCommandFromResource(int locationId, UpdateLocationStateResource resource)
    {
        var locationIdVo = new LocationId(locationId);
        return new UpdateLocationStateCommand(locationIdVo, resource.State);
    }
}