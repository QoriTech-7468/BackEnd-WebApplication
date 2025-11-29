using Rutana.API.IAM.Application.Internal.OutboundServices;
using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.IAM.Domain.Model.Commands;
using Rutana.API.IAM.Domain.Repositories;
using Rutana.API.IAM.Domain.Services;
using Rutana.API.Shared.Domain.Repositories;
using Rutana.API.Shared.Domain.Model.ValueObjects;

namespace Rutana.API.IAM.Application.Internal.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork)
    : IUserCommandService
{
    // --- INICIAR SESIÓN 
    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Email);

        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid username or password");

        var token = tokenService.GenerateToken(user);

        return (user, token);
    }

    // --- REGISTRO (Sign Up) ---
    public async Task <User> Handle(SignUpCommand command)
    {
        // 1. Verificar si el correo ya existe
        if (await userRepository.ExistsByUsername(command.Email))
            throw new Exception($"Email {command.Email} is already taken");

        // 2. Encriptar contraseña
        var hashedPassword = hashingService.HashPassword(command.Password);

        // 3. Crear Usuario 
        var organizationId = command.OrganizationId.HasValue 
            ? new OrganizationId(command.OrganizationId.Value) 
            : null;
            
        var user = new User(
            command.Name,
            command.Surname,
            command.Phone,
            command.Email,
            hashedPassword,
            command.Role, 
            organizationId); 

    try

    {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while creating user: {e.Message}");
        }
        return user;
    }
}