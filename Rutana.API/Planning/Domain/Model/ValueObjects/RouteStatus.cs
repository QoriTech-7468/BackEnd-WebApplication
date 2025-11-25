namespace Rutana.API.Planning.Domain.Model.ValueObjects;

/// <summary>
/// Represents the possible states of a route.
/// </summary>
public enum RouteStatus
{
    /// <summary>
    /// The route is in draft state and can be edited.
    /// </summary>
    Draft = 1,

    /// <summary>
    /// The route has been published and is active.
    /// </summary>
    Published = 2,

    /// <summary>
    /// The route has been completed.
    /// </summary>
    Completed = 3
}