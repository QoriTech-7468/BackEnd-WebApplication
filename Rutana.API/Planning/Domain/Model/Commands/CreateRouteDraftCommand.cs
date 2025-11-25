namespace Rutana.API.Planning.Domain.Model.Commands;

/// <summary>
/// Command to create a new route draft.
/// </summary>
/// <param name="OrganizationId">The organization identifier.</param>
/// <param name="ColorCode">The color code for route identification.</param>
public record CreateRouteDraftCommand(int OrganizationId, string ColorCode);