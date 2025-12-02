namespace Rutana.API.CRM.Interfaces.REST.Resources;

/// <summary>
/// Resource to update a client's state.
/// </summary>
/// <param name="State">The new state (enabled/disabled).</param>
public record UpdateClientStateResource(string State);