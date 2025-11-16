namespace Rutana.API.Suscriptions.Domain.Model.ValueObjects;

/// <summary>
/// Organization name value object.
/// </summary>
public sealed class OrganizationName
{
    public string Value { get; }

    private OrganizationName() => Value = string.Empty;

    private OrganizationName(string value)
    {
        Value = value;
    }

    public static OrganizationName From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Organization name cannot be empty.", nameof(value));

        return new OrganizationName(value.Trim());
    }

    public override string ToString() => Value;
}


