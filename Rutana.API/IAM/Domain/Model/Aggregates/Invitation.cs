using Rutana.API.IAM.Domain.Model.Enums;
using Rutana.API.Shared.Domain.Model.ValueObjects;

namespace Rutana.API.IAM.Domain.Model.Aggregates;

/// <summary>
///     Represents an invitation sent by an organization to a user.
/// </summary>
public class Invitation
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Invitation" /> class.
    /// </summary>
    private Invitation()
    {
    }

    private Invitation(OrganizationId organizationId, int userId, UserRole role)
    {
        OrganizationId = organizationId;
        UserId = userId;
        Role = role;
        Status = InvitationStatus.Pending;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    /// <summary>
    ///     Gets the invitation identifier (database identity).
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    ///     Gets the organization that sent the invitation.
    /// </summary>
    public OrganizationId OrganizationId { get; private set; } = null!;

    /// <summary>
    ///     Gets the user who received the invitation.
    /// </summary>
    public int UserId { get; private set; }

    /// <summary>
    ///     Gets the role assigned to the user if they accept the invitation.
    /// </summary>
    public UserRole Role { get; private set; }

    /// <summary>
    ///     Gets the status of the invitation.
    /// </summary>
    public InvitationStatus Status { get; private set; }

    /// <summary>
    ///     Gets the date and time when the invitation was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; private set; }

    /// <summary>
    ///     Factory method to create a new invitation.
    /// </summary>
    /// <param name="organizationId">The organization sending the invitation.</param>
    /// <param name="userId">The user receiving the invitation.</param>
    /// <param name="role">The role to assign to the user.</param>
    /// <returns>A new <see cref="Invitation" /> instance.</returns>
    public static Invitation Create(OrganizationId organizationId, int userId, UserRole role)
    {
        return new Invitation(organizationId, userId, role);
    }

    /// <summary>
    ///     Accepts the invitation.
    /// </summary>
    /// <returns>The updated <see cref="Invitation" /> instance.</returns>
    public Invitation Accept()
    {
        Status = InvitationStatus.Accepted;
        return this;
    }

    /// <summary>
    ///     Rejects the invitation.
    /// </summary>
    /// <returns>The updated <see cref="Invitation" /> instance.</returns>
    public Invitation Reject()
    {
        Status = InvitationStatus.Rejected;
        return this;
    }
}

