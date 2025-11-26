using Rutana.API.Planning.Domain.Model.ValueObjects;

namespace Rutana.API.Planning.Domain.Model.Entities;

/// <summary>
/// Represents a team member assigned to a route.
/// </summary>
public class RouteTeamMember
{
    /// <summary>
    /// Default constructor for EF Core.
    /// </summary>
    public RouteTeamMember()
    {
        Id = new RouteTeamMemberId();
        UserId = new UserId();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RouteTeamMember"/> class.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    public RouteTeamMember(UserId userId)
    {
        Id = new RouteTeamMemberId();
        UserId = userId;
    }

    /// <summary>
    /// Gets the unique identifier of the route team member.
    /// </summary>
    public RouteTeamMemberId Id { get; private set; }

    /// <summary>
    /// Gets the user identifier of the team member.
    /// </summary>
    /// <remarks>
    /// TODO: This references IAM bounded context (User aggregate) - currently using temporary UserId.
    /// </remarks>
    public UserId UserId { get; private set; }
}
