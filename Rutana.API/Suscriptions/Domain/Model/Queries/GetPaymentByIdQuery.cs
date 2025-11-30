namespace Rutana.API.Suscriptions.Domain.Model.Queries;

/// <summary>
/// Get Payment by Id query.
/// </summary>
/// <param name="Id">The identifier of the payment to retrieve.</param>

public record GetPaymentByIdQuery(int Id);