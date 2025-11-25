using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.IAM.Domain.Model.Queries;
using Rutana.API.IAM.Domain.Repositories;
using Rutana.API.IAM.Domain.Services;

namespace Rutana.API.IAM.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
    {
        return await userRepository.ListAsync();
    }

    public async Task<User?> Handle(GetUserByUsernameQuery query)
    {
        return await userRepository.FindByUsernameAsync(query.Username);
    }
    
    public async Task<IEnumerable<User>> Handle(GetUserByRoleQuery query)
    {
        //  lista vacía porque no hay roles
        return await Task.FromResult(Enumerable.Empty<User>());
    }
}