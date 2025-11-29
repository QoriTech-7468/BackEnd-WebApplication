namespace Rutana.API.Planning.Domain.Model.ValueObjects;

/// <summary>
/// Represents the possible states of a delivery.
/// </summary>
public enum DeliveryStatus
{
    /// <summary>
    /// The delivery is pending and has not started.
    /// </summary>
    Pending = 1,

    /// <summary>
    /// The delivery is currently in progress.
    /// </summary>
    InProgress = 2,

    /// <summary>
    /// The delivery has been completed successfully.
    /// </summary>
    Completed = 3,

    /// <summary>
    /// The delivery was rejected and could not be completed.
    /// </summary>
    Rejected = 4
}