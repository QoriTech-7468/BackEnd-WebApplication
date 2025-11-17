namespace Rutana.API.CRM.Domain.Model.ValueObjects;

/// <summary>
/// Client identifier value object.
/// </summary>
/// <param name="Value">The unique identifier for the client.</param>
public record ClientId(int Value)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ClientId"/> class with default value.
    /// </summary>
    public ClientId() : this(0)
    {
    }
}