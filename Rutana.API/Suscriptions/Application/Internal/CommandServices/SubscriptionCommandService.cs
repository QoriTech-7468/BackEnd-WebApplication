using Rutana.API.Shared.Domain.Repositories;
using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.Suscriptions.Domain.Model.Commands;
using Rutana.API.Suscriptions.Domain.Repositories;
using Rutana.API.Suscriptions.Domain.Services;

namespace Rutana.API.Suscriptions.Application.Internal.CommandServices;

public class SubscriptionCommandService(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork) 
    : ISubscriptionCommandService
{
    public async Task<Subscription> Handle(CreateSubscriptionCommand command)
    {
        var subscription = Subscription.Create(command);
        await subscriptionRepository.AddAsync(subscription);
        await unitOfWork.CompleteAsync();
        return subscription;
    }
}