namespace Rutana.API.Planning.Domain.Model.ValueObjects;

/// <summary>
/// Represents the identifier for a route team member in the business domain.
/// </summary>
/// <param name="Value">The unique identifier value.</param>
public record RouteTeamMemberId(int Value)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RouteTeamMemberId"/> class with default value.
    /// </summary>
    public RouteTeamMemberId() : this(0)
    {
    }
}