
using System.Text.Json.Serialization;

namespace Rutana.API.IAM.Domain.Model.Aggregates;

/// <summary>
///     Represents an application user.
/// </summary>
/// <remarks>
///     This aggregate encapsulates user identity details and exposes methods to update mutable fields.
///     Password hashes are ignored for JSON serialization.
/// </remarks>
public class User(string name, string surname, string phone, string email, string passwordHash)
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="User" /> class with empty values.
    /// </summary>
    public User() : this(string.Empty, string.Empty,string.Empty, string.Empty,string.Empty)
    {
    }

    /// <summary>
    ///     Gets the identifier of the user.
    /// </summary>
    public int Id { get; }

    /// <summary>
    ///     Gets the name.
    /// </summary>
    public string Name { get; private set; } = name;
    /// <summary>
    ///     Gets the surname.
    /// </summary>
    public string Surname { get; private set; } = surname;
    /// <summary>
    ///     Gets the phone.
    /// </summary>
    public string Phone { get; private set; } = phone;
    /// <summary>
    ///     Gets the email.
    /// </summary>
    public string Email { get; private set; } = email;
    

    /// <summary>
    ///     Gets the password hash. This property is ignored for JSON serialization.
    /// </summary>
    [JsonIgnore]
    public string PasswordHash { get; private set; } = passwordHash;

    /// <summary>
    ///     Update the user's username.
    /// </summary>
    /// <param name="username">The new name.</param>
    /// <returns>The updated <see cref="User" /> instance.</returns>
    public User UpdateUsername(string username)
    {
        Name = username;
        return this;
    }

    /// <summary>
    ///     Update the user's password hash.
    /// </summary>
    /// <param name="passwordHash">The new password hash.</param>
    /// <returns>The updated <see cref="User" /> instance.</returns>
    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }
}