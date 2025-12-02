namespace Rutana.API.Planning.Interfaces.REST.Resources;

/// <summary>
/// Resource for creating a new route draft.
/// </summary>
/// <param name="OrganizationId">The organization that will own the route draft.</param>
/// <param name="ColorCode">The color code for route identification.</param>
/// <param name="ExecutionDate">The date when the route will be executed.</param>
public record CreateRouteDraftResource(
    int OrganizationId,
    string ColorCode,
    DateTime ExecutionDate);