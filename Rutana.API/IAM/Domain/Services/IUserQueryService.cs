using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.IAM.Domain.Model.Queries;

namespace Rutana.API.IAM.Domain.Services;

/// <summary>
///     Contract for querying user information.
/// </summary>
public interface IUserQueryService
{
    /// <summary>
    ///     Handle a <see cref="GetUserByIdQuery" /> to retrieve a user by id.
    /// </summary>
    Task<User?> Handle(GetUserByIdQuery query);

    /// <summary>
    ///     Handle a <see cref="GetAllUsersQuery" /> to retrieve all users.
    /// </summary>
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);

    /// <summary>
    ///     Handle a <see cref="GetUserByUsernameQuery" /> to retrieve a user by username (Email).
    /// </summary>
    // ESTE FALTABA: Necesario para buscar por email
    Task<User?> Handle(GetUserByUsernameQuery query);

    /// <summary>
    ///     Handle a <see cref="GetUserByRoleQuery" /> to retrieve users by role.
    /// </summary>
    // CAMBIO AQUÍ: De 'User?' a 'IEnumerable<User>' (Devuelve una lista)
    Task<IEnumerable<User>> Handle(GetUserByRoleQuery query);

    /// <summary>
    ///     Handle a <see cref="GetUserByEmailQuery" /> to retrieve a user by email.
    /// </summary>
    Task<User?> Handle(GetUserByEmailQuery query);
}