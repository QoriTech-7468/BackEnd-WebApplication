using Rutana.API.IAM.Domain.Model.Commands;
using Rutana.API.IAM.Interfaces.REST.Resources;

namespace Rutana.API.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(
            resource.Name, 
            resource.Surname, 
            resource.Phone, 
            resource.Email, 
            resource.Password,
            resource.Role,           
            resource.OrganizationId  
        );
    }
}