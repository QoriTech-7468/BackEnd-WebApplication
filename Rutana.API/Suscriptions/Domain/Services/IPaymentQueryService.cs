using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.Suscriptions.Domain.Model.Queries;

namespace Rutana.API.Suscriptions.Domain.Services;

/// <summary>
/// Contract for payment query handling at the application layer.
/// </summary>
public interface IPaymentQueryService
{
    /// <summary>
    /// Handles the get payment by id query.
    /// </summary>
    /// <param name="query">The get payment by id query.</param>
    /// <returns>The matching <see cref="Payment" /> if found; otherwise, null.</returns>
    Task<Payment?> Handle(GetPaymentByIdQuery query);
}