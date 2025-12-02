using Rutana.API.CRM.Domain.Model.Commands;
using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.CRM.Interfaces.REST.Resources;

namespace Rutana.API.CRM.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert UpdateClientStatusResource to UpdateClientStateCommand.
/// </summary>
public static class UpdateClientStatusCommandFromResourceAssembler
{
    /// <summary>
    /// Convert resource to command.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <param name="resource">The update client status resource.</param>
    /// <returns>The update client state command.</returns>
    public static UpdateClientStateCommand ToCommandFromResource(int clientId, UpdateClientStatusResource resource)
    {
        var clientIdVo = new ClientId(clientId);
        var state = resource.IsActive ? "enabled" : "disabled";
        return new UpdateClientStateCommand(clientIdVo, state);
    }
}

