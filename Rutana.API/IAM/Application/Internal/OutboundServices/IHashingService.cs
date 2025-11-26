namespace Rutana.API.IAM.Application.Internal.OutboundServices;

public interface IHashingService
{
    // Método para encriptar la contraseña
    string HashPassword(string password);

    // Método para verificar si la contraseña coincide con el hash
    bool VerifyPassword(string password, string passwordHash);
}