using Rutana.API.IAM.Domain.Model.Commands;
using Rutana.API.IAM.Domain.Model.Enums;
using Rutana.API.IAM.Domain.Model.Queries;
using Rutana.API.IAM.Domain.Services;
using Rutana.API.IAM.Interfaces.ACL;

namespace Rutana.API.IAM.Application.ACL.Services;

public class IamContextFacade(IUserCommandService userCommandService, IUserQueryService userQueryService) : IIamContextFacade
{
    // ACTUALIZADO: Recibe los nuevos parámetros
    public async Task<int> CreateUser(string name, string surname, string phone, string email, string password, string role, int organizationId)
    {
        // Parse role from string to enum
        UserRole userRole = UserRole.NotAssigned;
        if (!string.IsNullOrWhiteSpace(role) && 
            Enum.TryParse<UserRole>(role, ignoreCase: true, out var parsedRole))
        {
            userRole = parsedRole;
        }
        
        // ACTUALIZADO: Se los pasa al SignUpCommand
        var signUpCommand = new SignUpCommand(name, surname, phone, email, password, userRole, organizationId);
        
        await userCommandService.Handle(signUpCommand);
        
        var getUserByUsernameQuery = new GetUserByUsernameQuery(email);
        var result = await userQueryService.Handle(getUserByUsernameQuery);
        return result?.Id ?? 0;
    }

    public async Task<int> FetchUserIdByUsername(string username)
    {
        var getUserByUsernameQuery = new GetUserByUsernameQuery(username);
        var result = await userQueryService.Handle(getUserByUsernameQuery);
        return result?.Id ?? 0;
    }

    public async Task<string> FetchUsernameByUserId(int userId)
    {
        var getUserByIdQuery = new GetUserByIdQuery(userId);
        var result = await userQueryService.Handle(getUserByIdQuery);
        return result?.Email ?? string.Empty;
    }
}