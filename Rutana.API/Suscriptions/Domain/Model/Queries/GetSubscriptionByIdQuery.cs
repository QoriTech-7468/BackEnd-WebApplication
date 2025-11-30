using Rutana.API.Suscriptions.Domain.Model.ValueObjects;

namespace Rutana.API.Suscriptions.Domain.Model.Queries;

public record GetSubscriptionByIdQuery(SubscriptionId Id);