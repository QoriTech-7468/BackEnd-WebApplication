using Rutana.API.CRM.Domain.Model.Commands;
using Rutana.API.CRM.Interfaces.REST.Resources;

namespace Rutana.API.CRM.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert RegisterClientResource to RegisterClientCommand.
/// </summary>
public static class RegisterClientCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a RegisterClientResource to a RegisterClientCommand.
    /// </summary>
    /// <param name="resource">The RegisterClientResource.</param>
    /// <returns>The RegisterClientCommand.</returns>
    public static RegisterClientCommand ToCommandFromResource(RegisterClientResource resource)
    {
        return new RegisterClientCommand(
            resource.OrganizationId,
            resource.CompanyName);
    }
}