namespace Rutana.API.CRM.Domain.Model.Queries;

/// <summary>
/// Query to get all locations belonging to a client.
/// </summary>
/// <param name="ClientId">The identifier of the client.</param>
public record GetLocationsByClientIdQuery(int ClientId);