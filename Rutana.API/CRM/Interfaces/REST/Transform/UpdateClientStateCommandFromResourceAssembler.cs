using Rutana.API.CRM.Domain.Model.Commands;
using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.CRM.Interfaces.REST.Resources;

namespace Rutana.API.CRM.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert UpdateClientStateResource to UpdateClientStateCommand.
/// </summary>
public static class UpdateClientStateCommandFromResourceAssembler
{
    /// <summary>
    /// Convert resource to command.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <param name="resource">The update client state resource.</param>
    /// <returns>The update client state command.</returns>
    public static UpdateClientStateCommand ToCommandFromResource(int clientId, UpdateClientStateResource resource)
    {
        var clientIdVo = new ClientId(clientId);
        return new UpdateClientStateCommand(clientIdVo, resource.State);
    }
}