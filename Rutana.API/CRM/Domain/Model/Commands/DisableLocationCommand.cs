namespace Rutana.API.CRM.Domain.Model.Commands;

/// <summary>
/// Command to disable a location.
/// </summary>
/// <param name="LocationId">The identifier of the location to disable.</param>
public record DisableLocationCommand(int LocationId);