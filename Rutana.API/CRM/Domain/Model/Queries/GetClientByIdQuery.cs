namespace Rutana.API.CRM.Domain.Model.Queries;

/// <summary>
/// Query to get a client by its identifier.
/// </summary>
/// <param name="ClientId">The identifier of the client.</param>
public record GetClientByIdQuery(int ClientId);