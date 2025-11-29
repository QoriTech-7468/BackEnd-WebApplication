using Rutana.API.IAM.Domain.Model.Commands;
using Rutana.API.IAM.Domain.Model.Enums;
using Rutana.API.IAM.Interfaces.REST.Resources;

namespace Rutana.API.IAM.Interfaces.REST.Transform;

public static class CreateUserCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(CreateUserResource resource)
    {
        // Parse role from string to enum
        UserRole role = UserRole.NotAssigned;
        if (!string.IsNullOrWhiteSpace(resource.Role) && 
            Enum.TryParse<UserRole>(resource.Role, ignoreCase: true, out var parsedRole))
        {
            role = parsedRole;
        }
        
        return new SignUpCommand(
            resource.Name,
            resource.Surname,
            resource.Phone,
            resource.Email,
            resource.Password,
            role,
            resource.OrganizationId
        );
    }
}