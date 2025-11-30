using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.Suscriptions.Domain.Model.Queries;

namespace Rutana.API.Suscriptions.Domain.Services;

public interface ISubscriptionQueryService
{
    Task<Subscription?> Handle(GetSubscriptionByIdQuery query);
}