namespace Rutana.API.Suscriptions.Interfaces.REST.Resources;

public record SubscriptionResource(
    int Id,
    string PlanType,
    decimal Price);

