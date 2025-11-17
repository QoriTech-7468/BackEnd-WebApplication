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
/// Location command service implementation.
/// </summary>
/// <param name="locationRepository">The location repository.</param>
/// <param name="unitOfWork">The unit of work.</param>
/// <param name="context">The database context.</param>
public class LocationCommandService(
    ILocationRepository locationRepository,
    IUnitOfWork unitOfWork,
    AppDbContext context) : ILocationCommandService
{
    /// <inheritdoc />
    public async Task<Location?> Handle(RegisterLocationCommand command)
    {
        // Verificar si ya existe una ubicación con el mismo nombre para el cliente
        var exists = await locationRepository.ExistsByNameAndClientIdAsync(
            command.Name, 
            command.ClientId);
        
        if (exists)
            throw new InvalidOperationException(
                $"A location with name '{command.Name}' already exists for this client.");

        var location = new Location(command);
        await locationRepository.AddAsync(location);
        await unitOfWork.CompleteAsync();
        
        // After SaveChanges, update the LocationId with the generated value
        // EF Core generates the ID in the database but doesn't update the value object automatically
        // We need to reload the entity to get the generated ID value
        await context.Entry(location).ReloadAsync();
        
        return location;
    }

    /// <inheritdoc />
    public async Task<Location?> Handle(EnableLocationCommand command)
    {
        var location = await locationRepository.FindByIdAsync(command.LocationId.Value);
        if (location is null)
            return null;

        location.Enable();
        locationRepository.Update(location);
        await unitOfWork.CompleteAsync();
        return location;
    }

    /// <inheritdoc />
    public async Task<Location?> Handle(DisableLocationCommand command)
    {
        var location = await locationRepository.FindByIdAsync(command.LocationId.Value);
        if (location is null)
            return null;

        location.Disable();
        locationRepository.Update(location);
        await unitOfWork.CompleteAsync();
        return location;
    }
}