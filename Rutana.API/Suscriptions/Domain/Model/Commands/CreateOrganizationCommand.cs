using Rutana.API.Suscriptions.Domain.Model.ValueObjects;

namespace Rutana.API.Suscriptions.Domain.Model.Commands;

/// <summary>
/// Create Organization command.
/// </summary>
/// <param name="Name">The organization name.</param>
/// <param name="Ruc">The organization RUC.</param>
/// <param name="UserId">The identifier of the user who will be assigned as Owner.</param>
public record CreateOrganizationCommand(
    string Name,
    string Ruc,
    int UserId);


