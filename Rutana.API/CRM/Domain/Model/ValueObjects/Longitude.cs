namespace Rutana.API.CRM.Domain.Model.ValueObjects;

/// <summary>
/// Longitude value object.
/// Represents a longitude coordinate.
/// </summary>
/// <param name="Value">The longitude value as a string.</param>
public record Longitude(string Value)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Longitude"/> class with default value.
    /// </summary>
    public Longitude() : this(string.Empty)
    {
    }

    /// <summary>
    /// Creates a new Longitude with validation.
    /// </summary>
    /// <param name="longitude">The longitude value as a string.</param>
    /// <returns>A new Longitude instance.</returns>
    /// <exception cref="ArgumentException">Thrown when longitude is null, whitespace, or invalid.</exception>
    public static Longitude Create(string longitude)
    {
        if (string.IsNullOrWhiteSpace(longitude))
            throw new ArgumentException("Longitude cannot be empty.", nameof(longitude));

        // Try to parse as double to validate it's a valid number
        if (!double.TryParse(longitude.Trim(), out double lonValue))
            throw new ArgumentException("Longitude must be a valid number.", nameof(longitude));

        // Validate longitude range: -180 to 180
        if (lonValue < -180 || lonValue > 180)
            throw new ArgumentException("Longitude must be between -180 and 180 degrees.", nameof(longitude));

        return new Longitude(longitude.Trim());
    }
}

