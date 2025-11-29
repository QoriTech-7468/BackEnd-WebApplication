namespace Rutana.API.IAM.Domain.Model.Enums;

/// <summary>
///     Represents the status of an invitation.
/// </summary>
public enum InvitationStatus
{
    /// <summary>
    ///     Invitation is pending acceptance or rejection.
    /// </summary>
    Pending = 0,
    
    /// <summary>
    ///     Invitation has been accepted.
    /// </summary>
    Accepted = 1,
    
    /// <summary>
    ///     Invitation has been rejected.
    /// </summary>
    Rejected = 2
}

