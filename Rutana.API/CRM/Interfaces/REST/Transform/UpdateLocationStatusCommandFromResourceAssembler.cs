using Rutana.API.CRM.Domain.Model.Commands;
using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.CRM.Interfaces.REST.Resources;

namespace Rutana.API.CRM.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert UpdateLocationStatusResource to UpdateLocationStateCommand.
/// </summary>
public static class UpdateLocationStatusCommandFromResourceAssembler
{
    /// <summary>
    /// Convert resource to command.
    /// </summary>
    /// <param name="locationId">The location identifier.</param>
    /// <param name="resource">The update location status resource.</param>
    /// <returns>The update location state command.</returns>
    public static UpdateLocationStateCommand ToCommandFromResource(int locationId, UpdateLocationStatusResource resource)
    {
        var locationIdVo = new LocationId(locationId);
        var state = resource.IsActive ? "enabled" : "disabled";
        return new UpdateLocationStateCommand(locationIdVo, state);
    }
}

