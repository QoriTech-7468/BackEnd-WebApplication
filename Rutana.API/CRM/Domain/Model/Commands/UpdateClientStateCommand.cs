using Rutana.API.CRM.Domain.Model.ValueObjects;

namespace Rutana.API.CRM.Domain.Model.Commands;

/// <summary>
/// Command to update a client's state.
/// </summary>
/// <param name="ClientId">The client identifier.</param>
/// <param name="State">The new state (enabled/disabled).</param>
public record UpdateClientStateCommand(ClientId ClientId, string State);