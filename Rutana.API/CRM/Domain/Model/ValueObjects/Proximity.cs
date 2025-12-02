namespace Rutana.API.CRM.Domain.Model.ValueObjects;

/// <summary>
/// Represents the proximity level of a location.
/// </summary>
public enum Proximity
{
    /// <summary>
    /// Close proximity - close to the organization.
    /// </summary>
    Close,

    /// <summary>
    /// Mid proximity - moderate distance.
    /// </summary>
    Mid,

    /// <summary>
    /// Far proximity - distant from the organization.
    /// </summary>
    Far
}
