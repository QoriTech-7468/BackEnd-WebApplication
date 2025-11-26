using Rutana.API.IAM.Domain.Model.Commands;
using Rutana.API.IAM.Interfaces.REST.Resources;

namespace Rutana.API.IAM.Interfaces.REST.Transform;

public static class CreateUserCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(CreateUserResource resource)
        => new SignUpCommand(
            resource.Name,
            resource.Surname,
            resource.Phone,
            resource.Email,
            resource.Password,
            resource.Role,
            resource.OrganizationId
        );
}