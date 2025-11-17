namespace Rutana.API.CRM.Domain.Model.ValueObjects;

/// <summary>
/// Represents the proximity level of a location.
/// </summary>
public enum Proximity
{
    /// <summary>
    /// Near proximity - close to the organization.
    /// </summary>
    Near,

    /// <summary>
    /// Medium proximity - moderate distance.
    /// </summary>
    Medium,

    /// <summary>
    /// Far proximity - distant from the organization.
    /// </summary>
    Far
}
