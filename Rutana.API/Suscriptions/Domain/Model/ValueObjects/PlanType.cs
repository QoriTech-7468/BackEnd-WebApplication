namespace Rutana.API.Suscriptions.Domain.Model.ValueObjects;

public record PlanType(string Value)
{
    public static PlanType Starter => new("Starter");
    public static PlanType Professional => new("Professional");
    public static PlanType Enterprise => new("Enterprise");

    public static PlanType From(string value)
    {
        return value.ToLower() switch
        {
            "starter" => Starter,
            "professional" => Professional,
            "enterprise" => Enterprise,
            _ => throw new ArgumentException("Invalid plan type")
        };
    }
}