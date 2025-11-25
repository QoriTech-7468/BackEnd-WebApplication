using Rutana.API.Planning.Domain.Model.Entities;
using Rutana.API.Planning.Interfaces.REST.Resources;

namespace Rutana.API.Planning.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert RouteTeamMember entity to RouteTeamMemberResource.
/// </summary>
public static class RouteTeamMemberResourceFromEntityAssembler
{
    /// <summary>
    /// Converts a RouteTeamMember entity to a RouteTeamMemberResource.
    /// </summary>
    /// <param name="entity">The RouteTeamMember entity.</param>
    /// <returns>The RouteTeamMemberResource.</returns>
    public static RouteTeamMemberResource ToResourceFromEntity(RouteTeamMember entity)
    {
        return new RouteTeamMemberResource(
            entity.Id.Value,
            entity.UserId.Value);
    }
}