using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.Suscriptions.Domain.Model.Queries;
using Rutana.API.Suscriptions.Domain.Repositories;
using Rutana.API.Suscriptions.Domain.Services;

namespace Rutana.API.Suscriptions.Application.Internal.QueryServices;

public class SubscriptionQueryService(ISubscriptionRepository subscriptionRepository) : ISubscriptionQueryService
{
    public async Task<Subscription?> Handle(GetSubscriptionByIdQuery query)
    {
        return await subscriptionRepository.FindByIdAsync((int)query.Id.Value);
    }
}