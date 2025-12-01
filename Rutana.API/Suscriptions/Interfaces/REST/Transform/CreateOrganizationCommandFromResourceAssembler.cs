using Rutana.API.Suscriptions.Domain.Model.Commands;
using Rutana.API.Suscriptions.Interfaces.REST.Resources;

namespace Rutana.API.Suscriptions.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create a <see cref="CreateOrganizationCommand" /> command from a resource
/// </summary>
public static class CreateOrganizationCommandFromResourceAssembler
{
    /// <summary>
    ///     Create a <see cref="CreateOrganizationCommand" /> command from a resource
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateOrganizationResource" /> resource to create the command from
    /// </param>
    /// <returns>
    ///     The <see cref="CreateOrganizationCommand" /> command created from the resource
    /// </returns>
    public static CreateOrganizationCommand ToCommandFromResource(CreateOrganizationResource resource)
    {
        return new CreateOrganizationCommand(
            resource.Name,
            resource.Ruc,
            resource.UserId
        );
    }
}

