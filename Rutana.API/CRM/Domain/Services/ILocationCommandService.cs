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
    /// Handles the update location state command.
    /// </summary>
    /// <param name="command">The update location state command.</param>
    /// <returns>The updated location.</returns>
    Task<Location?> Handle(UpdateLocationStateCommand command);

    /// <summary>
    /// Handles the update location command.
    /// </summary>
    /// <param name="command">The update location command.</param>
    /// <returns>The updated location.</returns>
    Task<Location?> Handle(UpdateLocationCommand command);
}