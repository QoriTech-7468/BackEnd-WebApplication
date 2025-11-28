using Rutana.API.IAM.Domain.Model.Commands;
using Rutana.API.IAM.Domain.Model.Enums;
using Rutana.API.IAM.Interfaces.REST.Resources;

namespace Rutana.API.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        // SignUp always uses default values: NotAssigned role and null OrganizationId
        return new SignUpCommand(
            resource.Name, 
            resource.Surname, 
            resource.Phone, 
            resource.Email, 
            resource.Password,
            UserRole.NotAssigned,  // Default role for new sign-ups
            null                    // Default OrganizationId (null) for new sign-ups
        );
    }
}