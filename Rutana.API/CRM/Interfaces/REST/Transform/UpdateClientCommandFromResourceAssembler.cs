using Rutana.API.CRM.Domain.Model.Commands;
using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.CRM.Interfaces.REST.Resources;

namespace Rutana.API.CRM.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert UpdateClientResource to UpdateClientCommand.
/// </summary>
public static class UpdateClientCommandFromResourceAssembler
{
    /// <summary>
    /// Convert resource to command.
    /// </summary>
    /// <param name="resource">The update client resource.</param>
    /// <returns>The update client command.</returns>
    public static UpdateClientCommand ToCommandFromResource(UpdateClientResource resource)
    {
        var clientIdVo = new ClientId(resource.Id);
        return new UpdateClientCommand(
            clientIdVo,
            resource.Name,
            resource.IsActive);
    }
}

