namespace Rutana.API.CRM.Domain.Model.ValueObjects;

/// <summary>
/// Location name value object.
/// </summary>
/// <param name="Value">The location name.</param>
public record LocationName(string Value)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocationName"/> class with default value.
    /// </summary>
    public LocationName() : this(string.Empty)
    {
    }

    /// <summary>
    /// Creates a new LocationName with validation.
    /// </summary>
    /// <param name="name">The location name.</param>
    /// <returns>A new LocationName instance.</returns>
    /// <exception cref="ArgumentException">Thrown when name is null or whitespace.</exception>
    public static LocationName Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Location name cannot be empty.", nameof(name));

        return new LocationName(name.Trim());
    }
}