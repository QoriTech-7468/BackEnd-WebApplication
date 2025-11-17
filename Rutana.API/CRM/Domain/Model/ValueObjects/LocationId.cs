namespace Rutana.API.CRM.Domain.Model.ValueObjects;

/// <summary>
/// Location identifier value object.
/// </summary>
/// <param name="Value">The unique identifier for the location.</param>
public record LocationId(int Value)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocationId"/> class with default value.
    /// </summary>
    public LocationId() : this(0)
    {
    }
}