using Rutana.API.IAM.Domain.Model.Commands;
using Rutana.API.IAM.Domain.Model.Enums;
using Rutana.API.IAM.Interfaces.REST.Resources;

namespace Rutana.API.IAM.Interfaces.REST.Transform;

public static class ChangeUserRoleCommandFromResourceAssembler
{
    public static ChangeUserRoleCommand ToCommandFromResource(int userId, ChangeUserRoleResource resource)
    {
        // Parse role from string to enum
        UserRole role = UserRole.NotAssigned;
        if (!string.IsNullOrWhiteSpace(resource.Role) && 
            Enum.TryParse<UserRole>(resource.Role, ignoreCase: true, out var parsedRole))
        {
            role = parsedRole;
        }

        return new ChangeUserRoleCommand(userId, role);
    }
}

