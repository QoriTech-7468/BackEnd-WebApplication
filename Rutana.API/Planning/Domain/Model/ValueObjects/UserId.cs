namespace Rutana.API.Planning.Domain.Model.ValueObjects;

/// <summary>
/// Represents the identifier for a user in the system.
/// </summary>
/// <remarks>
/// TODO: This is a temporary implementation. Replace with the actual UserId from IAM bounded context when available.
/// </remarks>
/// <param name="Value">The unique identifier value.</param>
public record UserId(int Value)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserId"/> class with default value.
    /// </summary>
    public UserId() : this(0)
    {
    }
}