namespace Rutana.API.IAM.Domain.Model.Enums;

/// <summary>
///     Represents the role of a user in the organization.
/// </summary>
public enum UserRole
{
    /// <summary>
    ///     User has not been assigned a role yet.
    /// </summary>
    NotAssigned = 0,
    
    /// <summary>
    ///     Administrator role with full access.
    /// </summary>
    Admin = 1,
    
    /// <summary>
    ///     Dispatcher role for managing routes and assignments.
    /// </summary>
    Dispatcher = 2,
    
    /// <summary>
    ///     Owner role for organization owners.
    /// </summary>
    Owner = 3
}

