namespace Rutana.API.Suscriptions.Domain.Model.Commands;

/// <summary>
/// Create Subscription command.
/// </summary>
/// <param name="PlanType">Plan type (Starter, Professional, Enterprise)</param>
/// <param name="Price">Plan price</param>
public record CreateSubscription(
    string PlanType,
    decimal Price);