using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.Suscriptions.Interfaces.REST.Resources;

namespace Rutana.API.Suscriptions.Interfaces.REST.Transform;

public static class SubscriptionResourceFromEntityAssembler
{
    public static SubscriptionResource ToResourceFromEntity(Subscription entity)
    {
        return new SubscriptionResource(
            (int)entity.Id.Value, 
            entity.PlanType.Value, 
            entity.Price
        );
    }
}

