using Rutana.API.IAM.Application.Internal.OutboundServices;
using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.IAM.Domain.Model.Commands;
using Rutana.API.IAM.Domain.Model.Enums;
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

    // --- ACTUALIZAR ORGANIZACIÓN Y ROLE DEL USUARIO ---
    public async Task<User> Handle(UpdateUserOrganizationAndRoleCommand command)
    {
        // 1. Buscar el usuario
        var user = await userRepository.FindByIdAsync(command.UserId);
        if (user == null)
        {
            throw new Exception($"User with id {command.UserId} not found");
        }

        // 2. Validar que el usuario no tenga organización asignada
        if (user.OrganizationId != null)
        {
            throw new Exception("User already belongs to an organization");
        }

        // 3. Actualizar el usuario con organizationId y role
        var organizationId = new OrganizationId(command.OrganizationId);
        user.UpdateOrganizationAndRole(organizationId, command.Role);

        try
        {
            userRepository.Update(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while updating user: {e.Message}");
        }

        return user;
    }

    // --- CAMBIAR ROLE DEL USUARIO ---
    public async Task<User> Handle(ChangeUserRoleCommand command)
    {
        // 1. Validar que el role sea Admin o Dispatcher (no Owner, no NotAssigned)
        if (command.Role != UserRole.Admin && command.Role != UserRole.Dispatcher)
        {
            throw new Exception("Role must be Admin or Dispatcher");
        }

        // 2. Buscar el usuario
        var user = await userRepository.FindByIdAsync(command.UserId);
        if (user == null)
        {
            throw new Exception($"User with id {command.UserId} not found");
        }

        // 3. Validar que el usuario no sea Owner
        if (user.Role == UserRole.Owner)
        {
            throw new Exception("Cannot change the role of an Owner");
        }

        // 4. Validar que el usuario tenga organización asignada
        if (user.OrganizationId == null)
        {
            throw new Exception("User does not belong to an organization");
        }

        // 5. Actualizar el role del usuario
        user.UpdateRole(command.Role);

        try
        {
            userRepository.Update(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while changing user role: {e.Message}");
        }

        return user;
    }

    // --- ELIMINAR USUARIO DE LA ORGANIZACIÓN ---
    public async Task<User> Handle(RemoveUserFromOrganizationCommand command)
    {
        // 1. Buscar el usuario
        var user = await userRepository.FindByIdAsync(command.UserId);
        if (user == null)
        {
            throw new Exception($"User with id {command.UserId} not found");
        }

        // 2. Validar que el usuario no sea Owner
        if (user.Role == UserRole.Owner)
        {
            throw new Exception("Cannot remove an Owner from the organization");
        }

        // 3. Validar que el usuario tenga organización asignada
        if (user.OrganizationId == null)
        {
            throw new Exception("User does not belong to an organization");
        }

        // 4. Remover el usuario de la organización (Role a NotAssigned, OrganizationId a null)
        user.RemoveFromOrganization();

        try
        {
            userRepository.Update(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while removing user from organization: {e.Message}");
        }

        return user;
    }
}