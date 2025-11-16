namespace Rutana.API.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Strongly typed identifier for Organization aggregate, backed by an integer
/// to support database-generated identity columns.
/// </summary>
public readonly record struct OrganizationId(int Value)
{
    public override string ToString() => Value.ToString();
}


