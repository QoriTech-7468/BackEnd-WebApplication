namespace Rutana.API.Planning.Domain.Model.ValueObjects;

/// <summary>
/// Represents the identifier for a route in the business domain.
/// </summary>
/// <param name="Value">The unique identifier value.</param>
public record RouteId(int Value)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RouteId"/> class with default value.
    /// </summary>
    public RouteId() : this(0)
    {
    }
}