namespace Rutana.API.Planning.Domain.Model.ValueObjects;

/// <summary>
/// Represents the possible states of a route.
/// </summary>
public enum RouteStatus
{
    /// <summary>
    /// The route has been published but has not started yet.
    /// </summary>
    NotStarted = 1,

    /// <summary>
    /// The route is currently in progress (execution has started).
    /// </summary>
    InProgress = 2,

    /// <summary>
    /// The route has been finished/completed.
    /// </summary>
    Finished = 3
}