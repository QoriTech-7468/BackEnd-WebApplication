using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.IAM.Interfaces.REST.Resources;
using Rutana.API.Suscriptions.Domain.Model.Aggregates;

namespace Rutana.API.IAM.Interfaces.REST.Transform;

public static class InvitationResourceFromEntityAssembler
{
    public static InvitationResource ToResourceFromEntity(Invitation entity, Organization? organization, User? user = null)
    {
        var organizationName = organization?.Name.Value ?? "Unknown Organization";
        var userName = user?.Name ?? "Unknown";
        var userSurname = user?.Surname ?? "Unknown";
        var userEmail = user?.Email ?? "Unknown";
        
        return new InvitationResource(
            entity.Id,
            organizationName,
            entity.UserId,
            userName,
            userSurname,
            userEmail,
            entity.Role.ToString(),
            entity.Status.ToString(),
            entity.CreatedAt
        );
    }

    public static InvitationSummaryResource ToSummaryResourceFromEntity(Invitation entity, Organization? organization)
    {
        var organizationName = organization?.Name.Value ?? "Unknown Organization";
        
        return new InvitationSummaryResource(
            entity.Id,
            organizationName,
            entity.Role.ToString(),
            entity.Status.ToString(),
            entity.CreatedAt
        );
    }
}

