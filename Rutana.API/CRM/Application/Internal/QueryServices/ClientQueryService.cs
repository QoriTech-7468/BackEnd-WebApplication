using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Domain.Model.Queries;
using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.CRM.Domain.Repositories;
using Rutana.API.CRM.Domain.Services;

namespace Rutana.API.CRM.Application.Internal.QueryServices;

/// <summary>
/// Client query service implementation.
/// </summary>
/// <param name="clientRepository">The client repository.</param>
public class ClientQueryService(IClientRepository clientRepository) : IClientQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Client>> Handle(GetClientsByOrganizationIdQuery query)
    {
        return await clientRepository.FindByOrganizationIdAsync(query.OrganizationId);
    }

    /// <inheritdoc />
    public async Task<Client?> Handle(GetClientByIdQuery query)
    {
        return await clientRepository.FindByIdAsync(query.ClientId.Value);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Client>> Handle(GetAllClientsQuery query)
    {
        return await clientRepository.FindAllAsync(query.IsActive);
    }
}