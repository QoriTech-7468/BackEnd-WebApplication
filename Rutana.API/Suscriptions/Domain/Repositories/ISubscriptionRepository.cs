using Rutana.API.Shared.Domain.Repositories;
using Rutana.API.Suscriptions.Domain.Model.Aggregates;

namespace Rutana.API.Suscriptions.Domain.Repositories;

public interface ISubscriptionRepository : IBaseRepository<Subscription>
{
}