using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.IAM.Interfaces.REST.Resources;

namespace Rutana.API.IAM.Interfaces.REST.Transform;

public static class InvitationResourceFromEntityAssembler
{
    public static InvitationResource ToResourceFromEntity(Invitation entity)
    {
        return new InvitationResource(
            entity.Id,
            entity.OrganizationId.Value,
            entity.UserId,
            entity.Role.ToString(),
            entity.Status.ToString(),
            entity.CreatedAt
        );
    }

    public static InvitationSummaryResource ToSummaryResourceFromEntity(Invitation entity)
    {
        return new InvitationSummaryResource(
            entity.Id,
            entity.OrganizationId.Value,
            entity.Role.ToString(),
            entity.Status.ToString(),
            entity.CreatedAt
        );
    }
}

