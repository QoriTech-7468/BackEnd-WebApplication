using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Interfaces.REST.Resources;

namespace Rutana.API.CRM.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert Client entity to ClientResource.
/// </summary>
public static class ClientResourceFromEntityAssembler
{
    /// <summary>
    /// Converts a Client entity to a ClientResource.
    /// </summary>
    /// <param name="entity">The Client entity.</param>
    /// <returns>The ClientResource.</returns>
    public static ClientResource ToResourceFromEntity(Client entity)
    {
        return new ClientResource(
            entity.Id.Value,
            entity.CompanyName.Value,
            entity.IsEnabled);
    }
}