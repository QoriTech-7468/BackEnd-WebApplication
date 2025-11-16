using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Suscriptions.Domain.Model.ValueObjects;

namespace Rutana.API.Suscriptions.Domain.Model.Aggregates;

/// <summary>
/// Organization aggregate root.
/// </summary>
public partial class Organization
{
    public OrganizationId Id { get; private set; }
    public OrganizationName Name { get; private set; } = null!;
    public Ruc Ruc { get; private set; } = null!;

    private Organization() { }

    private Organization(OrganizationId id, OrganizationName name, Ruc ruc)
    {
        Id = id;
        Name = name;
        Ruc = ruc;
    }

    /// <summary>
    /// Factory method aligned with the CreateOrganization command from the diagram.
    /// </summary>
    public static Organization Create(OrganizationId id, OrganizationName name, Ruc ruc)
        => new(id, name, ruc);

    public void Rename(OrganizationName newName)
    {
        Name = newName;
    }
}


