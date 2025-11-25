using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Domain.Model.Commands;

namespace Rutana.API.CRM.Domain.Services;

/// <summary>
/// Represents the location command service in the Rutana CRM System.
/// </summary>
public interface ILocationCommandService
{
    /// <summary>
    /// Handles the register location command.
    /// </summary>
    /// <param name="command">The register location command.</param>
    /// <returns>The created location.</returns>
    Task<Location?> Handle(RegisterLocationCommand command);

    /// <summary>
    /// Handles the enable location command.
    /// </summary>
    /// <param name="command">The enable location command.</param>
    /// <returns>The enabled location.</returns>
    Task<Location?> Handle(EnableLocationCommand command);

    /// <summary>
    /// Handles the disable location command.
    /// </summary>
    /// <param name="command">The disable location command.</param>
    /// <returns>The disabled location.</returns>
    Task<Location?> Handle(DisableLocationCommand command);
}