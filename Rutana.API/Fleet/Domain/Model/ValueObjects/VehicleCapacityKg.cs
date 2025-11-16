namespace Rutana.API.Fleet.Domain.Model.ValueObjects;

/// <summary>
/// Vehicle capacity in kilograms value object.
/// </summary>
/// <param name="Value">The capacity in kilograms.</param>
public record VehicleCapacityKg(decimal Value)
{
    /// <summary>
    /// Initializes a new instance with zero capacity.
    /// </summary>
    public VehicleCapacityKg() : this(0)
    {
    }

    /// <summary>
    /// Validates and creates a VehicleCapacityKg instance.
    /// </summary>
    /// <param name="capacityKg">The capacity to validate.</param>
    /// <returns>A validated VehicleCapacityKg instance.</returns>
    /// <exception cref="ArgumentException">Thrown when capacity is invalid.</exception>
    public static VehicleCapacityKg Create(decimal capacityKg)
    {
        if (capacityKg <= 0)
            throw new ArgumentException("Vehicle capacity must be greater than zero.", nameof(capacityKg));

        if (capacityKg > 50000) // Maximum 50 tons
            throw new ArgumentException("Vehicle capacity cannot exceed 50,000 kg.", nameof(capacityKg));

        return new VehicleCapacityKg(capacityKg);
    }
}