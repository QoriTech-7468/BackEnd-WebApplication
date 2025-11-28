using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.IAM.Domain.Model.Queries;
using Rutana.API.IAM.Domain.Repositories;
using Rutana.API.IAM.Domain.Services;
using Rutana.API.Shared.Domain.Model.ValueObjects;

namespace Rutana.API.IAM.Application.Internal.QueryServices;

public class InvitationQueryService(IInvitationRepository invitationRepository) : IInvitationQueryService
{
    public async Task<Invitation?> Handle(GetInvitationByIdQuery query)
    {
        return await invitationRepository.FindByIdAsync(query.InvitationId);
    }

    public async Task<IEnumerable<Invitation>> Handle(GetInvitationsByUserIdQuery query)
    {
        return await invitationRepository.FindPendingByUserIdAsync(query.UserId);
    }

    public async Task<IEnumerable<Invitation>> Handle(GetInvitationsByOrganizationIdQuery query)
    {
        var organizationId = new OrganizationId(query.OrganizationId);
        return await invitationRepository.FindPendingByOrganizationIdAsync(organizationId);
    }
}

