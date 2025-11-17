using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Interfaces.REST.Resources;

namespace Rutana.API.CRM.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert Client entity to ClientSummaryResource.
/// </summary>
public static class ClientSummaryResourceFromEntityAssembler
{
    /// <summary>
    /// Converts a Client entity to a ClientSummaryResource.
    /// </summary>
    /// <param name="entity">The Client entity.</param>
    /// <returns>The ClientSummaryResource.</returns>
    public static ClientSummaryResource ToResourceFromEntity(Client entity)
    {
        return new ClientSummaryResource(
            entity.Id,
            entity.CompanyName.Value);
    }
}