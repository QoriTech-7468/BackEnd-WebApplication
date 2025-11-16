namespace Rutana.API.CRM.Domain.Model.ValueObjects;

/// <summary>
/// Organization identifier value object.
/// </summary>
/// <param name="Value">The unique identifier for the organization.</param>
public record OrganizationId(int Value)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationId"/> class with default value.
    /// </summary>
    public OrganizationId() : this(0)
    {
    }
}