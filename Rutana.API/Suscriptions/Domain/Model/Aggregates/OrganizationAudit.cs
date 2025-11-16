namespace Rutana.API.Suscriptions.Domain.Model.Aggregates;

/// <summary>
/// Audit information for an organization aggregate.
/// Modeled as an owned/value type inside the aggregate.
/// </summary>
public class OrganizationAudit
{
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private OrganizationAudit() { }

    private OrganizationAudit(DateTime createdAt, DateTime updatedAt)
    {
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static OrganizationAudit CreateNew()
        => new(DateTime.UtcNow, DateTime.UtcNow);

    public void Touch()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}


