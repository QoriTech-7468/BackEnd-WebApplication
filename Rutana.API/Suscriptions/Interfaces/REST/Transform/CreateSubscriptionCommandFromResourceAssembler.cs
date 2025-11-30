using Rutana.API.Suscriptions.Domain.Model.Commands;
using Rutana.API.Suscriptions.Interfaces.REST.Resources;


namespace Rutana.API.Suscriptions.Interfaces.REST.Transform;

public static class CreateSubscriptionCommandFromResourceAssembler
{
    public static CreateSubscriptionCommand ToCommandFromResource(CreateSubscriptionResource resource)
    {
        return new CreateSubscriptionCommand(resource.PlanType, resource.Price);
    }
}

