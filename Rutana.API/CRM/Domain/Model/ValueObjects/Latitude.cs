namespace Rutana.API.CRM.Domain.Model.ValueObjects;

/// <summary>
/// Latitude value object.
/// Represents a latitude coordinate.
/// </summary>
/// <param name="Value">The latitude value as a string.</param>
public record Latitude(string Value)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Latitude"/> class with default value.
    /// </summary>
    public Latitude() : this(string.Empty)
    {
    }

    /// <summary>
    /// Creates a new Latitude with validation.
    /// </summary>
    /// <param name="latitude">The latitude value as a string.</param>
    /// <returns>A new Latitude instance.</returns>
    /// <exception cref="ArgumentException">Thrown when latitude is null, whitespace, or invalid.</exception>
    public static Latitude Create(string latitude)
    {
        if (string.IsNullOrWhiteSpace(latitude))
            throw new ArgumentException("Latitude cannot be empty.", nameof(latitude));

        // Try to parse as double to validate it's a valid number
        if (!double.TryParse(latitude.Trim(), out double latValue))
            throw new ArgumentException("Latitude must be a valid number.", nameof(latitude));

        // Validate latitude range: -90 to 90
        if (latValue < -90 || latValue > 90)
            throw new ArgumentException("Latitude must be between -90 and 90 degrees.", nameof(latitude));

        return new Latitude(latitude.Trim());
    }
}

