using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.IAM.Interfaces.REST.Resources;

namespace Rutana.API.IAM.Interfaces.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User user, string token)
    {
        return new AuthenticatedUserResource(
            user.Id, 
            user.Name, 
            user.Surname, 
            token, 
            user.OrganizationId?.Value, 
            user.Role.ToString()
        );
    }
}