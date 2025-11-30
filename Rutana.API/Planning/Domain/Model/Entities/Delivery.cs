using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.Planning.Domain.Model.ValueObjects;

namespace Rutana.API.Planning.Domain.Model.Entities;

/// <summary>
/// Represents a delivery entity within a route.
/// </summary>
public class Delivery
{
    /// <summary>
    /// Default constructor for EF Core.
    /// </summary>
    public Delivery()
    {
        Id = new DeliveryId();
        LocationId = new LocationId();
        Status = DeliveryStatus.Pending;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Delivery"/> class.
    /// </summary>
    /// <param name="locationId">The location identifier for this delivery.</param>
    public Delivery(LocationId locationId)
    {
        Id = new DeliveryId();
        LocationId = locationId;
        Status = DeliveryStatus.Pending;
        RejectReason = null;
        RejectDetails = null;
    }

    /// <summary>
    /// Gets the unique identifier of the delivery.
    /// </summary>
    public DeliveryId Id { get; private set; }

    /// <summary>
    /// Gets the location identifier where the delivery should be made.
    /// </summary>
    /// <remarks>
    /// References CRM bounded context (Location aggregate).
    /// </remarks>
    public LocationId LocationId { get; private set; }

    /// <summary>
    /// Gets the current status of the delivery.
    /// </summary>
    public DeliveryStatus Status { get; private set; }

    /// <summary>
    /// Gets the reason for rejection if the delivery was rejected.
    /// </summary>
    public RejectReason? RejectReason { get; private set; }

    /// <summary>
    /// Gets additional details about the rejection.
    /// </summary>
    public string? RejectDetails { get; private set; }

    /// <summary>
    /// Marks the delivery as in progress.
    /// </summary>
    public void MarkAsInProgress()
    {
        if (Status != DeliveryStatus.Pending)
            throw new InvalidOperationException("Only pending deliveries can be marked as in progress.");

        Status = DeliveryStatus.InProgress;
    }

    /// <summary>
    /// Marks the delivery as completed.
    /// </summary>
    public void MarkAsCompleted()
    {
        if (Status != DeliveryStatus.InProgress)
            throw new InvalidOperationException("Only in-progress deliveries can be marked as completed.");

        Status = DeliveryStatus.Completed;
    }

    /// <summary>
    /// Marks the delivery as rejected with a reason and details.
    /// </summary>
    /// <param name="reason">The reason for rejection.</param>
    /// <param name="details">Additional details about the rejection.</param>
    public void MarkAsRejected(RejectReason reason, string? details = null)
    {
        if (Status == DeliveryStatus.Completed)
            throw new InvalidOperationException("Completed deliveries cannot be rejected.");

        Status = DeliveryStatus.Rejected;
        RejectReason = reason;
        RejectDetails = details;
    }
}