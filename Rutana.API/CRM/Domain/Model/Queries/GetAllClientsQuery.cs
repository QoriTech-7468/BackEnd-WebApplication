namespace Rutana.API.CRM.Domain.Model.Queries;

/// <summary>
/// Query to get all clients, optionally filtered by active status.
/// </summary>
/// <param name="IsActive">Optional filter by active status. If null, returns all clients.</param>
public record GetAllClientsQuery(bool? IsActive = null);

