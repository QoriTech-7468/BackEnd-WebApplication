namespace Rutana.API.Fleet.Domain.Model.ValueObjects;

/// <summary>
/// Vehicle identifier value object.
/// </summary>
/// <param name="Value">The unique identifier for the vehicle.</param>
public record VehicleId(int Value)
{
    /// <summary>
    /// Initializes a new instance with zero (for new entities).
    /// </summary>
    public VehicleId() : this(0)
    {
    }
}