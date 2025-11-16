namespace Rutana.API.CRM.Domain.Model.Commands;

/// <summary>
/// Command to enable a location.
/// </summary>
/// <param name="LocationId">The identifier of the location to enable.</param>
public record EnableLocationCommand(int LocationId);