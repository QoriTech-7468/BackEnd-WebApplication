using System.Text.Json.Serialization;
using Rutana.API.Shared.Domain.Model.ValueObjects;

namespace Rutana.API.IAM.Domain.Model.Aggregates;

/// <summary>
///     Represents an application user.
/// </summary>
/// <remarks>
///     This aggregate encapsulates user identity details and exposes methods to update mutable fields.
///     Password hashes are ignored for JSON serialization.
/// </remarks>
public class User(
    string name,
    string surname,
    string phone,
    string email,
    string passwordHash,
    string role,
    OrganizationId organizationId)
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="User" /> class with empty values.
    /// </summary>
    public User() : this(
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        new OrganizationId(0))
    {
    }

    /// <summary>
    ///     Gets the user identifier (database identity).
    /// </summary>
    public int Id { get; }

    /// <summary>
    ///     Gets the user's first name.
    /// </summary>
    public string Name { get; private set; } = name;

    /// <summary>
    ///     Gets the user's surname.
    /// </summary>
    public string Surname { get; private set; } = surname;

    /// <summary>
    ///     Gets the user's phone number.
    /// </summary>
    public string Phone { get; private set; } = phone;

    /// <summary>
    ///     Gets the user's email address.
    /// </summary>
    public string Email { get; private set; } = email;

    /// <summary>
    ///     Gets the role of the user in the organization.
    /// </summary>
    public string Role { get; private set; } = role;

    /// <summary>
    ///     Gets the organization the user is a part of.
    /// </summary>
    public OrganizationId OrganizationId { get; private set; } = organizationId;

    /// <summary>
    ///     Gets the password hash. This property is ignored for JSON serialization.
    /// </summary>
    [JsonIgnore]
    public string PasswordHash { get; private set; } = passwordHash;

    /// <summary>
    ///     Updates the user's username (name).
    /// </summary>
    /// <param name="username">The new username.</param>
    /// <returns>The updated <see cref="User" /> instance.</returns>
    public User UpdateUsername(string username)
    {
        Name = username;
        return this;
    }

    /// <summary>
    ///     Updates the user's password hash.
    /// </summary>
    /// <param name="passwordHash">The new password hash.</param>
    /// <returns>The updated <see cref="User" /> instance.</returns>
    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }
}