using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.Suscriptions.Interfaces.REST.Resources;

namespace Rutana.API.Suscriptions.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create an <see cref="OrganizationResource" /> resource from an entity
/// </summary>
public static class OrganizationResourceFromEntityAssembler
{
    /// <summary>
    ///     Create an <see cref="OrganizationResource" /> resource from an entity
    /// </summary>
    /// <param name="organization">
    ///     The <see cref="Organization" /> entity to create the resource from
    /// </param>
    /// <returns>
    ///     The <see cref="OrganizationResource" /> resource created from the entity
    /// </returns>
    public static OrganizationResource ToResourceFromEntity(Organization organization)
    {
        return new OrganizationResource(
            organization.Id.Value,
            organization.Name.Value,
            organization.Ruc.Value
        );
    }
}

