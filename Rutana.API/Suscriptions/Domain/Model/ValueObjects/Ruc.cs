namespace Rutana.API.Suscriptions.Domain.Model.ValueObjects;

/// <summary>
/// RUC value object (tax identifier) for an organization.
/// </summary>
public sealed class Ruc
{
    public string Value { get; }

    private Ruc() => Value = string.Empty;

    private Ruc(string value)
    {
        Value = value;
    }

    public static Ruc From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("RUC cannot be empty.", nameof(value));

        return new Ruc(value.Trim());
    }

    public override string ToString() => Value;
}


