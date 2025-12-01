
using Rutana.API.Suscriptions.Domain.Model.ValueObjects;

namespace Rutana.API.Suscriptions.Domain.Model.Aggregates;



/// <summary>
/// Payment aggregate root.
/// </summary>
public partial class Payment
{
    public int Id { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = string.Empty;
    public string Status { get; private set; } = string.Empty;
    public int UserId { get; private set; }
    
    public SubscriptionId SubscriptionId { get; private set; }
    private Payment() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Payment"/> class.
    /// </summary>
    /// <param name="amount">The amount of money.</param>
    /// <param name="currency">The currency code (e.g., PEN).</param>
    /// <param name="userId">The ID of the user making the payment.</param>
    
    public Payment(decimal amount, string currency, int userId, SubscriptionId subscriptionId)
    {
        Amount = amount;
        Currency = currency;
        UserId = userId;
        SubscriptionId = SubscriptionId;
        Status = "Completed"; 
    }
}