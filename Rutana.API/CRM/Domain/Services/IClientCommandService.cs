using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Domain.Model.Commands;

namespace Rutana.API.CRM.Domain.Services;

/// <summary>
/// Represents the client command service in the Rutana CRM System.
/// </summary>
public interface IClientCommandService
{
    /// <summary>
    /// Handles the register client command.
    /// </summary>
    /// <param name="command">The register client command.</param>
    /// <returns>The created client.</returns>
    Task<Client?> Handle(RegisterClientCommand command);

    /// <summary>
    /// Handles the update client state command.
    /// </summary>
    /// <param name="command">The update client state command.</param>
    /// <returns>The updated client.</returns>
    Task<Client?> Handle(UpdateClientStateCommand command);
}