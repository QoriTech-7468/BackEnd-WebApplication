namespace Rutana.API.CRM.Domain.Model.Queries;

/// <summary>
/// Query to get a location by its identifier.
/// </summary>
/// <param name="LocationId">The identifier of the location.</param>
public record GetLocationByIdQuery(int LocationId);
