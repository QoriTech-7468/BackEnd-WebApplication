namespace Rutana.API.Planning.Domain.Model.ValueObjects;

/// <summary>
/// Represents the identifier for a delivery in the business domain.
/// </summary>
/// <param name="Value">The unique identifier value.</param>
public record DeliveryId(int Value)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeliveryId"/> class with default value.
    /// </summary>
    public DeliveryId() : this(0)
    {
    }
}