namespace Rutana.API.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Strongly typed identifier for Organization aggregate that can be shared across bounded contexts.
/// </summary>
public readonly record struct OrganizationId(Guid Value)
{
    public static OrganizationId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();
}


