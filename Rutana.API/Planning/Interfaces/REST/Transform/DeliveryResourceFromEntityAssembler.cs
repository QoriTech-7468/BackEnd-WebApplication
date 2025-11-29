using Rutana.API.Planning.Domain.Model.Entities;
using Rutana.API.Planning.Interfaces.REST.Resources;

namespace Rutana.API.Planning.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert Delivery entity to DeliveryResource.
/// </summary>
public static class DeliveryResourceFromEntityAssembler
{
    /// <summary>
    /// Converts a Delivery entity to a DeliveryResource.
    /// </summary>
    /// <param name="entity">The Delivery entity.</param>
    /// <returns>The DeliveryResource.</returns>
    public static DeliveryResource ToResourceFromEntity(Delivery entity)
    {
        return new DeliveryResource(
            entity.Id.Value,
            entity.LocationId.Value,
            entity.Status.ToString(),
            entity.RejectReason?.ToString(),
            entity.RejectDetails);
    }
}