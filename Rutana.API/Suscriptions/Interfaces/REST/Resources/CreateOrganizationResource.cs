namespace Rutana.API.Suscriptions.Interfaces.REST.Resources;

/// <summary>
///     Resource for creating a new organization
/// </summary>
/// <param name="Name">
///     The name of the organization
/// </param>
/// <param name="Ruc">
///     The RUC of the organization
/// </param>
/// <param name="UserId">
///     The identifier of the user who will be assigned as Owner of the organization
/// </param>
public record CreateOrganizationResource(
    string Name,
    string Ruc,
    int UserId);

