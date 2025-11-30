namespace Rutana.API.Suscriptions.Interfaces.REST.Resources;

/// <summary>
///     Resource representing a payment.
/// </summary>
/// <param name="Id">The unique identifier of the payment.</param>
/// <param name="Amount">The amount of money paid.</param>
/// <param name="Currency">The currency code (e.g., PEN).</param>
/// <param name="Status">The current status of the payment.</param>
/// <param name="UserId">The identifier of the user who made the payment.</param>
public record PaymentResource(
    int Id,
    decimal Amount,
    string Currency,
    string Status,
    int UserId);