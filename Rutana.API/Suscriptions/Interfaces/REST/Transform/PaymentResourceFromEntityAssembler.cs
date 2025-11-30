using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.Suscriptions.Interfaces.REST.Resources;

namespace Rutana.API.Suscriptions.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create a <see cref="PaymentResource" /> from an entity.
/// </summary>
public static class PaymentResourceFromEntityAssembler
{
    /// <summary>
    ///     Create a <see cref="PaymentResource" /> from a Payment entity.
    /// </summary>
    /// <param name="entity">The <see cref="Payment" /> entity.</param>
    /// <returns>The resource created from the entity.</returns>
    public static PaymentResource ToResourceFromEntity(Payment entity)
    {
        return new PaymentResource(
            entity.Id,
            entity.Amount,
            entity.Currency,
            entity.Status,
            entity.UserId
        );
    }
}