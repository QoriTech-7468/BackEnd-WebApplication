using Rutana.API.Suscriptions.Domain.Model.Commands;

namespace Rutana.API.Suscriptions.Domain.Application.Services;

/// <summary>
/// Contract for organization command handling at the application layer.
/// </summary>
public interface IOrganizationCommandService
{
    /// <summary>
    /// Handles the create organization command.
    /// </summary>
    /// <param name="command">The create organization command.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Handle(CreateOrganizationCommand command);
}


