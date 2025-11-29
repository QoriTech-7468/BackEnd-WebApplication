namespace Rutana.API.Planning.Domain.Model.ValueObjects;

/// <summary>
/// Represents the possible reasons for delivery rejection.
/// </summary>
public enum RejectReason
{
    /// <summary>
    /// The client was not available at the delivery location.
    /// </summary>
    ClientNotAvailable = 1,

    /// <summary>
    /// The address provided was incorrect or not found.
    /// </summary>
    AddressIncorrect = 2,

    /// <summary>
    /// There was a safety issue preventing delivery.
    /// </summary>
    SafetyIssue = 3,

    /// <summary>
    /// Other reason not specified above.
    /// </summary>
    Other = 4
}