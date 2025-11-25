using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Suscriptions.Domain.Model.Commands;
using Rutana.API.Suscriptions.Domain.Model.ValueObjects;

namespace Rutana.API.Suscriptions.Domain.Model.Aggregates;

/// <summary>
/// Organization aggregate root.
/// </summary>
public partial class Organization
{
    public OrganizationId Id { get; private set; } = new OrganizationId(0);
    public OrganizationName Name { get; private set; } = null!;
    public Ruc Ruc { get; private set; } = null!;

    private Organization() { }

    private Organization(OrganizationName name, Ruc ruc)
    {
        Name = name;
        Ruc = ruc;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Organization"/> class from a create organization command.
    /// </summary>
    /// <param name="command">The create organization command.</param>
    public Organization(CreateOrganizationCommand command) : this(
        OrganizationName.From(command.Name),
        Ruc.From(command.Ruc))
    {
    }

    /// <summary>
    /// Factory method to create a new organization.
    /// </summary>
    /// <param name="command">The create organization command.</param>
    /// <returns>A new Organization instance.</returns>
    public static Organization Create(CreateOrganizationCommand command)
    {
        return new Organization(command);
    }

    public void Rename(OrganizationName newName)
    {
        Name = newName;
    }
}


