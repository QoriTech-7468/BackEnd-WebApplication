using Rutana.API.Suscriptions.Domain.Model.ValueObjects;

namespace Rutana.API.Suscriptions.Domain.Model.Aggregates;

/// <summary>
/// Organization aggregate root.
/// </summary>
public class Organization
{
    public OrganizationId Id { get; private set; }
    public OrganizationName Name { get; private set; } = null!;
    public Ruc Ruc { get; private set; } = null!;
    public OrganizationAudit Audit { get; private set; } = null!;

    private Organization() { }

    private Organization(OrganizationId id, OrganizationName name, Ruc ruc, OrganizationAudit audit)
    {
        Id = id;
        Name = name;
        Ruc = ruc;
        Audit = audit;
    }

    /// <summary>
    /// Factory method aligned with the CreateOrganization command from the diagram.
    /// </summary>
    public static Organization Create(OrganizationId id, OrganizationName name, Ruc ruc)
    {
        var audit = OrganizationAudit.CreateNew();
        return new Organization(id, name, ruc, audit);
    }

    public void Rename(OrganizationName newName)
    {
        Name = newName;
        Audit.Touch();
    }
}


