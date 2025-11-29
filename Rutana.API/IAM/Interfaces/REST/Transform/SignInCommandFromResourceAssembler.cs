using Rutana.API.IAM.Domain.Model.Commands;
using Rutana.API.IAM.Interfaces.REST.Resources;

namespace Rutana.API.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Email, resource.Password);
    }
}