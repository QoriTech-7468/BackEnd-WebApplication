namespace Rutana.API.Fleet.Domain.Model.ValueObjects;

/// <summary>
/// License plate value object.
/// </summary>
/// <param name="Value">The license plate number.</param>
public record LicensePlate(string Value)
{
    /// <summary>
    /// Initializes a new instance with an empty string.
    /// </summary>
    public LicensePlate() : this(string.Empty)
    {
    }

    /// <summary>
    /// Validates and creates a LicensePlate instance.
    /// </summary>
    /// <param name="plate">The plate number to validate.</param>
    /// <returns>A validated LicensePlate instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the plate is invalid.</exception>
    public static LicensePlate Create(string plate)
    {
        if (string.IsNullOrWhiteSpace(plate))
            throw new ArgumentException("License plate cannot be empty.", nameof(plate));

        // Remove spaces and convert to uppercase for consistency
        var normalizedPlate = plate.Trim().ToUpperInvariant();

        if (normalizedPlate.Length < 6 || normalizedPlate.Length > 10)
            throw new ArgumentException("License plate must be between 6 and 10 characters.", nameof(plate));

        return new LicensePlate(normalizedPlate);
    }
}