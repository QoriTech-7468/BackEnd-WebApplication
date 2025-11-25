namespace Rutana.API.Suscriptions.Interfaces.REST.Resources;

/// <summary>
///     Resource representing an organization
/// </summary>
/// <param name="Id">
///     The unique identifier of the organization
/// </param>
/// <param name="Name">
///     The name of the organization
/// </param>
/// <param name="Ruc">
///     The RUC of the organization
/// </param>
public record OrganizationResource(
    int Id,
    string Name,
    string Ruc);

