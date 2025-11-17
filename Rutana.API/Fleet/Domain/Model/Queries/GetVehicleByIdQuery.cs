namespace Rutana.API.Fleet.Domain.Model.Queries;

/// <summary>
/// Query to get a vehicle by its identifier.
/// </summary>
/// <param name="VehicleId">The identifier of the vehicle.</param>
public record GetVehicleByIdQuery(int VehicleId);