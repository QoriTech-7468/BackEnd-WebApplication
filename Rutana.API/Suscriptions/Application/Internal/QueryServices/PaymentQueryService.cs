using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.Suscriptions.Domain.Model.Queries;
using Rutana.API.Suscriptions.Domain.Repositories;
using Rutana.API.Suscriptions.Domain.Services;

namespace Rutana.API.Suscriptions.Application.Internal.QueryServices;

/// <summary>
///     Application-level query service for the <see cref="Payment" /> aggregate.
/// </summary>
/// <param name="paymentRepository">
///     The <see cref="IPaymentRepository" /> used to read <see cref="Payment" /> instances.
/// </param>
public class PaymentQueryService(IPaymentRepository paymentRepository)
    : IPaymentQueryService
{
    /// <summary>
    ///     Handles the <see cref="GetPaymentByIdQuery" /> by delegating to the
    ///     <see cref="IPaymentRepository" /> and returning the matching payment, if any.
    /// </summary>
    /// <param name="query">
    ///     The query containing the <see cref="GetPaymentByIdQuery.Id" /> to search for.
    /// </param>
    /// <returns>
    ///     The matching <see cref="Payment" /> if one exists; otherwise, <c>null</c>.
    /// </returns>
    public async Task<Payment?> Handle(GetPaymentByIdQuery query)
    {
        return await paymentRepository.FindByIdAsync(query.Id);
    }
}