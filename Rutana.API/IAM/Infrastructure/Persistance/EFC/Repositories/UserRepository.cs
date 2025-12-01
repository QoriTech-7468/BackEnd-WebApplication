using Microsoft.EntityFrameworkCore;
using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.IAM.Domain.Repositories;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Rutana.API.IAM.Infrastructure.Persistance.EFC.Repositories;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> FindByUsernameAsync(string username)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user => user.Email.Equals(username));
    }

    public async Task<bool> ExistsByUsername(string username)
    {
        return await Context.Set<User>().AnyAsync(u => u.Email.Equals(username));
    }

    public async Task<IEnumerable<User>> FindByOrganizationIdAsync(OrganizationId organizationId)
    {
        // OrganizationId has HasConversion - must compare the value object directly, not .Value
        // Even though OrganizationId is nullable in User, EF Core can handle the comparison correctly
        return await Context.Set<User>()
            .Where(user => user.OrganizationId == organizationId)
            .ToListAsync();
    }
}