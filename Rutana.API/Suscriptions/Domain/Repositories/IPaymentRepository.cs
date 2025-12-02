using Rutana.API.Shared.Domain.Repositories;
using Rutana.API.Suscriptions.Domain.Model.Aggregates;

namespace Rutana.API.Suscriptions.Domain.Repositories;

/// <summary>
/// Payment repository contract for the Subscriptions bounded context.
/// </summary>
public interface IPaymentRepository : IBaseRepository<Payment>
{
 
}