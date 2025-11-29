using Rutana.API.Fleet.Domain.Model.Aggregates;
using Rutana.API.Fleet.Domain.Model.Commands;
using Rutana.API.Fleet.Domain.Repositories;
using Rutana.API.Fleet.Domain.Services;
using Rutana.API.Shared.Domain.Repositories;

namespace Rutana.API.Fleet.Application.Internal.CommandServices;

/// <summary>
/// Vehicle command service implementation.
/// </summary>
/// <param name="vehicleRepository">The vehicle repository.</param>
/// <param name="unitOfWork">The unit of work.</param>
public class VehicleCommandService(
    IVehicleRepository vehicleRepository,
    IUnitOfWork unitOfWork) : IVehicleCommandService
{
    /// <inheritdoc />
    public async Task<Vehicle?> Handle(RegisterVehicleCommand command)
    {
        var vehicle = new Vehicle(command);
        try
        {
            await vehicleRepository.AddAsync(vehicle);
            await unitOfWork.CompleteAsync();
            return vehicle;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<Vehicle?> Handle(UpdateVehicleProfileCommand command)
    {
        var vehicle = await vehicleRepository.FindByIdAsync(command.VehicleId);
        if (vehicle is null) return null;

        vehicle.UpdateProfile(command);

        try
        {
            vehicleRepository.Update(vehicle);
            await unitOfWork.CompleteAsync();
            return vehicle;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<Vehicle?> Handle(UpdateVehicleStateCommand command)
    {
        var vehicle = await vehicleRepository.FindByIdAsync(command.VehicleId);
        if (vehicle is null) return null;

        try
        {
            vehicle.UpdateState(command);
            vehicleRepository.Update(vehicle);
            await unitOfWork.CompleteAsync();
            return vehicle;
        }
        catch (ArgumentException)
        {
            return null; // Invalid state
        }
        catch (InvalidOperationException)
        {
            return vehicle; // Already in that state, return vehicle anyway
        }
        catch (Exception)
        {
            return null;
        }
    }
}