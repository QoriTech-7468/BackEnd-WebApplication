using Microsoft.EntityFrameworkCore;
using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Domain.Model.Commands;
using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.CRM.Domain.Repositories;
using Rutana.API.CRM.Domain.Services;
using Rutana.API.Shared.Domain.Repositories;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace Rutana.API.CRM.Application.Internal.CommandServices;

/// <summary>
/// Client command service implementation.
/// </summary>
/// <param name="clientRepository">The client repository.</param>
/// <param name="unitOfWork">The unit of work.</param>
/// <param name="context">The database context.</param>
public class ClientCommandService(
    IClientRepository clientRepository,
    IUnitOfWork unitOfWork,
    AppDbContext context) : IClientCommandService
{
    /// <inheritdoc />
    public async Task<Client?> Handle(RegisterClientCommand command)
    {
        // Verificar si ya existe un cliente con el mismo nombre en la organización
        var exists = await clientRepository.ExistsByCompanyNameAndOrganizationIdAsync(
            command.CompanyName, 
            command.OrganizationId);
        
        if (exists)
            throw new InvalidOperationException(
                $"A client with company name '{command.CompanyName}' already exists in this organization.");

        var client = new Client(command);
        await clientRepository.AddAsync(client);
        await unitOfWork.CompleteAsync();
        
        // After SaveChanges, update the ClientId with the generated value
        // EF Core generates the ID in the database but doesn't update the value object automatically
        // We need to reload the entity to get the generated ID value
        await context.Entry(client).ReloadAsync();
        
        return client;
    }

    /// <inheritdoc />
    public async Task<Client?> Handle(EnableClientCommand command)
    {
        var client = await clientRepository.FindByIdAsync(command.ClientId.Value);
        if (client is null)
            return null;

        client.Enable();
        clientRepository.Update(client);
        await unitOfWork.CompleteAsync();
        return client;
    }

    /// <inheritdoc />
    public async Task<Client?> Handle(DisableClientCommand command)
    {
        var client = await clientRepository.FindByIdAsync(command.ClientId.Value);
        if (client is null)
            return null;

        client.Disable();
        clientRepository.Update(client);
        await unitOfWork.CompleteAsync();
        return client;
    }
}