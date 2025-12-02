namespace Rutana.API.Suscriptions.Interfaces.REST.Resources;

public record CreateSubscriptionResource(
    string PlanType,
    decimal Price);

