namespace Rutana.API.CRM.Domain.Model.Queries;

/// <summary>
/// Query to get all locations, optionally filtered by active status and client ID.
/// </summary>
/// <param name="IsActive">Optional filter by active status. If null, returns all locations.</param>
/// <param name="ClientId">Optional filter by client ID. If null, returns locations for all clients.</param>
public record GetAllLocationsQuery(bool? IsActive = null, int? ClientId = null);

