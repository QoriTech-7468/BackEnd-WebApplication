namespace Rutana.API.Suscriptions.Domain.Model.ValueObjects;

/// <summary>
/// Strongly typed identifier for Organization aggregate.
/// </summary>
public readonly record struct OrganizationId(Guid Value)
{
    public static OrganizationId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();
}


