using Rutana.API.Fleet.Domain.Model.Aggregates;
using Rutana.API.Fleet.Domain.Model.Commands;
using Rutana.API.Fleet.Domain.Model.ValueObjects;
using Rutana.API.Fleet.Domain.Repositories;
using Rutana.API.Fleet.Domain.Services;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Shared.Domain.Repositories;

namespace Rutana.API.Fleet.Application.Internal.CommandServices;

/// <summary>
/// Vehicle command service implementation.
/// Handles all commands related to vehicle management.
/// </summary>
/// <param name="vehicleRepository">The vehicle repository.</param>
/// <param name="unitOfWork">The unit of work for transaction management.</param>
public class VehicleCommandService(
    IVehicleRepository vehicleRepository,
    IUnitOfWork unitOfWork)
    : IVehicleCommandService
{
    /// <inheritdoc />
    public async Task<Vehicle?> Handle(RegisterVehicleCommand command)
    {
        // Create value objects from command
        var plate = LicensePlate.Create(command.Plate);
        var organizationId = new OrganizationId(command.OrganizationId);
        
        // Validate that the plate doesn't already exist in the organization
        var existingVehicle = await vehicleRepository.FindByPlateAndOrganizationIdAsync(
            plate, 
            organizationId);

        if (existingVehicle != null)
            throw new InvalidOperationException(
                $"A vehicle with plate '{command.Plate}' already exists in this organization.");

        // Create the new vehicle
        var vehicle = Vehicle.Create(command);
        
        // Add to repository
        await vehicleRepository.AddAsync(vehicle);
        
        // Save changes
        await unitOfWork.CompleteAsync();

        return vehicle;
    }

    /// <inheritdoc />
    public async Task<Vehicle?> Handle(EnableVehicleCommand command)
    {
        // Find the vehicle
        var vehicle = await vehicleRepository.FindByIdAsync(command.VehicleId);
        
        if (vehicle == null)
            return null;

        // Enable the vehicle
        vehicle.Enable();
        
        // Update repository
        vehicleRepository.Update(vehicle);
        
        // Save changes
        await unitOfWork.CompleteAsync();

        return vehicle;
    }

    /// <inheritdoc />
    public async Task<Vehicle?> Handle(DisableVehicleCommand command)
    {
        // Find the vehicle
        var vehicle = await vehicleRepository.FindByIdAsync(command.VehicleId);
        
        if (vehicle == null)
            return null;

        // Disable the vehicle
        vehicle.Disable();
        
        // Update repository
        vehicleRepository.Update(vehicle);
        
        // Save changes
        await unitOfWork.CompleteAsync();

        return vehicle;
    }

    /// <inheritdoc />
    public async Task<Vehicle?> Handle(UpdateVehicleProfileCommand command)
    {
        // Find the vehicle
        var vehicle = await vehicleRepository.FindByIdAsync(command.VehicleId);
        
        if (vehicle == null)
            return null;

        // Create value objects from command
        var newPlate = LicensePlate.Create(command.Plate);
        
        // Check if the new plate already exists in another vehicle of the same organization
        var existingVehicle = await vehicleRepository.FindByPlateAndOrganizationIdAsync(
            newPlate, 
            vehicle.OrganizationId);

        if (existingVehicle != null && existingVehicle.Id != command.VehicleId)
            throw new InvalidOperationException(
                $"Another vehicle with plate '{command.Plate}' already exists in this organization.");

        // Update the vehicle profile
        vehicle.UpdateProfile(command);
        
        // Update repository
        vehicleRepository.Update(vehicle);
        
        // Save changes
        await unitOfWork.CompleteAsync();

        return vehicle;
    }
}