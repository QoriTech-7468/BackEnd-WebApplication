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
    /// Handles the enable client command.
    /// </summary>
    /// <param name="command">The enable client command.</param>
    /// <returns>The enabled client.</returns>
    Task<Client?> Handle(EnableClientCommand command);

    /// <summary>
    /// Handles the disable client command.
    /// </summary>
    /// <param name="command">The disable client command.</param>
    /// <returns>The disabled client.</returns>
    Task<Client?> Handle(DisableClientCommand command);
}