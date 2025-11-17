namespace Rutana.API.CRM.Domain.Model.ValueObjects;

/// <summary>
/// Company name value object.
/// </summary>
/// <param name="Value">The company name.</param>
public record CompanyName(string Value)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CompanyName"/> class with default value.
    /// </summary>
    public CompanyName() : this(string.Empty)
    {
    }

    /// <summary>
    /// Creates a new CompanyName with validation.
    /// </summary>
    /// <param name="name">The company name.</param>
    /// <returns>A new CompanyName instance.</returns>
    /// <exception cref="ArgumentException">Thrown when name is null or whitespace.</exception>
    public static CompanyName Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Company name cannot be empty.", nameof(name));

        return new CompanyName(name.Trim());
    }
}