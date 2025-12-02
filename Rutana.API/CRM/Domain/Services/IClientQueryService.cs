using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Domain.Model.Queries;

namespace Rutana.API.CRM.Domain.Services;

/// <summary>
/// Represents the client query service in the Rutana CRM System.
/// </summary>
public interface IClientQueryService
{
    /// <summary>
    /// Handles the get clients by organization id query.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>A collection of clients.</returns>
    Task<IEnumerable<Client>> Handle(GetClientsByOrganizationIdQuery query);

    /// <summary>
    /// Handles the get client by id query.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>The client if found, otherwise null.</returns>
    Task<Client?> Handle(GetClientByIdQuery query);

    /// <summary>
    /// Handles the get all clients query.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>A collection of clients.</returns>
    Task<IEnumerable<Client>> Handle(GetAllClientsQuery query);
}