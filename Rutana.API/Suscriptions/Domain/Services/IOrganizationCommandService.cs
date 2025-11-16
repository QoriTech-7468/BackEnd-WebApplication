using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.Suscriptions.Domain.Model.Commands;

namespace Rutana.API.Suscriptions.Domain.Services;

/// <summary>
/// Contract for organization command handling at the application layer.
/// </summary>
public interface IOrganizationCommandService
{
    /// <summary>
    /// Handles the create organization command.
    /// </summary>
    /// <param name="command">The create organization command.</param>
    /// <returns>The created <see cref="Organization" /> if successful; otherwise, null.</returns>
    Task<Organization?> Handle(CreateOrganizationCommand command);
}


