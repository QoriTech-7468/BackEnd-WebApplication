using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.Suscriptions.Domain.Model.Commands;

namespace Rutana.API.Suscriptions.Domain.Services;

public interface ISubscriptionCommandService
{
    Task<Subscription> Handle(CreateSubscription command);
}