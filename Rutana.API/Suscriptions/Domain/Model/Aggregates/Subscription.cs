using Rutana.API.Suscriptions.Domain.Model.ValueObjects;
using Rutana.API.Suscriptions.Domain.Model.Commands;

namespace Rutana.API.Suscriptions.Domain.Model.Aggregates;

public partial class Subscription
{
    public SubscriptionId Id { get; private set; } = new SubscriptionId(0);
    public PlanType PlanType { get; private set; } = null!;
    public decimal Price { get; private set; }

    private Subscription() {}

    private Subscription(PlanType planType, decimal price)
    {
        PlanType = planType;
        Price = price;
    }

    public Subscription(CreateSubscription command) : this(
        PlanType.From(command.PlanType),
        command.Price)
    {}

    public static Subscription Create(CreateSubscription command)
    {
        return new Subscription(command);
    }

    public void ChangePlan(PlanType newPlan, decimal newPrice)
    {
        PlanType = newPlan;
        Price = newPrice;
    }
}