using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.IAM.Interfaces.REST.Resources;

namespace Rutana.API.IAM.Interfaces.REST.Transform;

public static class UserByEmailResourceFromEntityAssembler
{
    public static UserByEmailResource ToResourceFromEntity(User entity)
    {
        return new UserByEmailResource(
            entity.Id,
            entity.Email,
            entity.Name,
            entity.Surname
        );
    }
}

