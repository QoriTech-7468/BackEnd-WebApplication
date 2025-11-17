using Rutana.API.CRM.Domain.Model.ValueObjects;

namespace Rutana.API.CRM.Domain.Model.Commands;

/// <summary>
/// Command to enable a client.
/// </summary>
/// <param name="ClientId">The identifier of the client to enable.</param>
public record EnableClientCommand(ClientId ClientId);