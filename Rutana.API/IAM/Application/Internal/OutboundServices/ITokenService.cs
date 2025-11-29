using Rutana.API.IAM.Domain.Model.Aggregates;

namespace Rutana.API.IAM.Application.Internal.OutboundServices;

public interface ITokenService
{
    // Genera el token 
    string GenerateToken(User user);

    // Valida el token 
    Task<int?> ValidateToken(string token);
}