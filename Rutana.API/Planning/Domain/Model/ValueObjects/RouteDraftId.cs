namespace Rutana.API.Planning.Domain.Model.ValueObjects;

/// <summary>
/// Represents the identifier for a route draft in the business domain.
/// </summary>
/// <param name="Value">The unique identifier value.</param>
public record RouteDraftId(int Value)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RouteDraftId"/> class with default value.
    /// </summary>
    public RouteDraftId() : this(0)
    {
    }
}