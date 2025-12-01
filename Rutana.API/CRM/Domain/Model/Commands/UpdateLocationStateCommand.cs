using Rutana.API.CRM.Domain.Model.ValueObjects;

namespace Rutana.API.CRM.Domain.Model.Commands;

/// <summary>
/// Command to update a location's state.
/// </summary>
/// <param name="LocationId">The location identifier.</param>
/// <param name="State">The new state (enabled/disabled).</param>
public record UpdateLocationStateCommand(LocationId LocationId, string State);