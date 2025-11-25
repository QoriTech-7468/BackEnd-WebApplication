namespace Rutana.API.Planning.Interfaces.REST.Resources;

/// <summary>
/// Delivery resource for REST API responses.
/// </summary>
/// <param name="Id">The unique identifier of the delivery.</param>
/// <param name="LocationId">The location identifier.</param>
/// <param name="Status">The current status of the delivery.</param>
/// <param name="RejectReason">The reason for rejection if rejected.</param>
/// <param name="RejectDetails">Additional details about the rejection.</param>
public record DeliveryResource(
    int Id,
    int LocationId,
    string Status,
    string? RejectReason,
    string? RejectDetails);