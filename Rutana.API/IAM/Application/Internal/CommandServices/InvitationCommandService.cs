using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.IAM.Domain.Model.Commands;
using Rutana.API.IAM.Domain.Model.Enums;
using Rutana.API.IAM.Domain.Repositories;
using Rutana.API.IAM.Domain.Services;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Shared.Domain.Repositories;

namespace Rutana.API.IAM.Application.Internal.CommandServices;

public class InvitationCommandService(
    IInvitationRepository invitationRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IInvitationCommandService
{
    public async Task<Invitation> Handle(CreateInvitationCommand command)
    {
        // 1. Validar que el role sea Admin o Dispatcher (no Owner, no NotAssigned)
        if (command.Role != UserRole.Admin && command.Role != UserRole.Dispatcher)
        {
            throw new Exception("Invitation role must be Admin or Dispatcher");
        }

        // 2. Buscar usuario por email
        var user = await userRepository.FindByUsernameAsync(command.UserEmail);
        if (user == null)
        {
            throw new Exception($"User with email {command.UserEmail} not found");
        }

        // 3. Validar que el usuario no tenga role Owner
        if (user.Role == UserRole.Owner)
        {
            throw new Exception("Cannot invite a user with Owner role");
        }

        // 4. Validar que el usuario no tenga organizationId asignado
        if (user.OrganizationId != null)
        {
            throw new Exception("User already belongs to an organization");
        }

        // 5. Validar que no exista una invitación pendiente para este usuario y organización
        var organizationId = new OrganizationId(command.OrganizationId);
        var existingInvitation = await invitationRepository.FindByUserIdAndOrganizationIdAsync(user.Id, organizationId);
        if (existingInvitation != null && existingInvitation.Status == InvitationStatus.Pending)
        {
            throw new Exception("A pending invitation already exists for this user and organization");
        }

        // 6. Crear la invitación
        var invitation = Invitation.Create(organizationId, user.Id, command.Role);

        try
        {
            await invitationRepository.AddAsync(invitation);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while creating invitation: {e.Message}");
        }

        return invitation;
    }

    public async Task<Invitation> Handle(AcceptInvitationCommand command)
    {
        // 1. Buscar la invitación
        var invitation = await invitationRepository.FindByIdAsync(command.InvitationId);
        if (invitation == null)
        {
            throw new Exception($"Invitation with id {command.InvitationId} not found");
        }

        // 2. Validar que la invitación esté pendiente
        if (invitation.Status != InvitationStatus.Pending)
        {
            throw new Exception("Invitation is not pending");
        }

        // 3. Buscar el usuario
        var user = await userRepository.FindByIdAsync(invitation.UserId);
        if (user == null)
        {
            throw new Exception($"User with id {invitation.UserId} not found");
        }

        // 4. Validar que el usuario no tenga organización asignada
        if (user.OrganizationId != null)
        {
            throw new Exception("User already belongs to an organization");
        }

        // 5. Actualizar el usuario con organizationId y role
        user.UpdateOrganizationAndRole(invitation.OrganizationId, invitation.Role);

        // 6. Marcar la invitación como aceptada
        invitation.Accept();

        try
        {
            userRepository.Update(user);
            invitationRepository.Update(invitation);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while accepting invitation: {e.Message}");
        }

        return invitation;
    }
}

