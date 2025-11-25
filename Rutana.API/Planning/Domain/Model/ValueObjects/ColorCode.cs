namespace Rutana.API.Planning.Domain.Model.ValueObjects;

/// <summary>
/// Represents a color code for route identification.
/// </summary>
/// <param name="Value">The color code value (e.g., "#FF5733").</param>
public record ColorCode(string Value)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ColorCode"/> class with default value.
    /// </summary>
    public ColorCode() : this(string.Empty)
    {
    }

    /// <summary>
    /// Creates a new ColorCode with validation.
    /// </summary>
    /// <param name="value">The color code value.</param>
    /// <returns>A new ColorCode instance.</returns>
    /// <exception cref="ArgumentException">Thrown when value is invalid.</exception>
    public static ColorCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Color code cannot be empty.", nameof(value));

        // Validate hex color format (basic validation)
        if (!value.StartsWith("#") || (value.Length != 7 && value.Length != 4))
            throw new ArgumentException("Color code must be in hex format (e.g., #FF5733).", nameof(value));

        return new ColorCode(value.ToUpper());
    }
}