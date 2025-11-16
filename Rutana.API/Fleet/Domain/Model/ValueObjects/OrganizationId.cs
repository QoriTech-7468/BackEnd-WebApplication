namespace Rutana.API.Fleet.Domain.Model.ValueObjects;

/// <summary>
/// Organization identifier value object.
/// </summary>
/// <param name="Value">The unique identifier for the organization.</param>
public record OrganizationId(int Value)
{
    /// <summary>
    /// Initializes a new instance with an empty GUID.
    /// </summary>
    public OrganizationId() : this(0)
    {
    }
}