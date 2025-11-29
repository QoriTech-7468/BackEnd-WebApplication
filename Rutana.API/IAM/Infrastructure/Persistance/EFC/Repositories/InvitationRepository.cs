using Microsoft.EntityFrameworkCore;
using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.IAM.Domain.Model.Enums;
using Rutana.API.IAM.Domain.Repositories;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Rutana.API.IAM.Infrastructure.Persistance.EFC.Repositories;

public class InvitationRepository(AppDbContext context) : BaseRepository<Invitation>(context), IInvitationRepository
{
    public async Task<Invitation?> FindByUserIdAndOrganizationIdAsync(int userId, OrganizationId organizationId)
    {
        // OrganizationId has HasConversion - must compare the value object directly, not .Value
        return await Context.Set<Invitation>()
            .FirstOrDefaultAsync(i => i.UserId == userId && i.OrganizationId == organizationId);
    }

    public async Task<IEnumerable<Invitation>> FindPendingByUserIdAsync(int userId)
    {
        return await Context.Set<Invitation>()
            .Where(i => i.UserId == userId && i.Status == InvitationStatus.Pending)
            .ToListAsync();
    }

    public async Task<IEnumerable<Invitation>> FindPendingByOrganizationIdAsync(OrganizationId organizationId)
    {
        // OrganizationId has HasConversion - must compare the value object directly, not .Value
        return await Context.Set<Invitation>()
            .Where(i => i.OrganizationId == organizationId && i.Status == InvitationStatus.Pending)
            .ToListAsync();
    }
}

