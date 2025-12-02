using Rutana.API.IAM.Interfaces.ACL;
using Rutana.API.Planning.Application.Internal.OutboundServices;

namespace Rutana.API.Planning.Infrastructure.OutboundServices;

/// <summary>
/// Outbound service implementation for IAM bounded context operations.
/// Wraps the IAM Context Facade to provide a single point of contact for Planning.
/// </summary>
/// <param name="iamContextFacade">The IAM context facade.</param>
public class IamService(IIamContextFacade iamContextFacade) : IIamService
{
    /// <inheritdoc />
    public async Task<bool> ExistsUserByIdAsync(int userId)
    {
        var username = await iamContextFacade.FetchUsernameByUserId(userId);
        return !string.IsNullOrEmpty(username);
    }

    /// <inheritdoc />
    public async Task<string?> GetUsernameByUserIdAsync(int userId)
    {
        var username = await iamContextFacade.FetchUsernameByUserId(userId);
        return string.IsNullOrEmpty(username) ? null : username;
    }
}