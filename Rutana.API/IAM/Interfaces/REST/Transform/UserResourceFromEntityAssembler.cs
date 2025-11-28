using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.IAM.Interfaces.REST.Resources;

namespace Rutana.API.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User entity)
    {
        return new UserResource(
            entity.Id, 
            entity.Name, 
            entity.Surname, 
            entity.Phone, 
            entity.Email,
            entity.Role.ToString(),           
            entity.OrganizationId?.Value ?? 0  
        );
    }
}