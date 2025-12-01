namespace Rutana.API.CRM.Domain.Model.ValueObjects;

/// <summary>
/// Address value object.
/// Represents a physical address for a location.
/// </summary>
/// <param name="Value">The address.</param>
public record Address(string Value)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Address"/> class with default value.
    /// </summary>
    public Address() : this(string.Empty)
    {
    }

    /// <summary>
    /// Creates a new Address with validation.
    /// </summary>
    /// <param name="address">The address.</param>
    /// <returns>A new Address instance.</returns>
    /// <exception cref="ArgumentException">Thrown when address is null or whitespace.</exception>
    public static Address Create(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address cannot be empty.", nameof(address));

        return new Address(address.Trim());
    }
}

