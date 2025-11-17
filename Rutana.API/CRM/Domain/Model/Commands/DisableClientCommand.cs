using Rutana.API.CRM.Domain.Model.ValueObjects;

namespace Rutana.API.CRM.Domain.Model.Commands;

/// <summary>
/// Command to disable a client.
/// </summary>
/// <param name="ClientId">The identifier of the client to disable.</param>
public record DisableClientCommand(ClientId ClientId);