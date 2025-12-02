using Microsoft.EntityFrameworkCore;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.Suscriptions.Domain.Model.ValueObjects;
using Rutana.API.Suscriptions.Domain.Repositories;

namespace Rutana.API.Suscriptions.Infrastructure.Persistence.EFC.Repositories;

public class SubscriptionRepository(AppDbContext context) : BaseRepository<Subscription>(context), ISubscriptionRepository
{
    public async override Task<Subscription?> FindByIdAsync(int id)
    {
        var subscriptionId = new SubscriptionId(id);
        return await Context.Set<Subscription>().FindAsync(subscriptionId);
    }
}