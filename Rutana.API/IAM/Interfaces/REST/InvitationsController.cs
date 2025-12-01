using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.IAM.Domain.Model.Commands;
using Rutana.API.IAM.Domain.Model.Enums;
using Rutana.API.IAM.Domain.Model.Queries;
using Rutana.API.IAM.Domain.Repositories;
using Rutana.API.IAM.Domain.Services;
using Rutana.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using Rutana.API.IAM.Interfaces.REST.Resources;
using Rutana.API.IAM.Interfaces.REST.Transform;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Shared.Domain.Repositories;
using Rutana.API.Suscriptions.Domain.Model.Queries;
using Rutana.API.Suscriptions.Domain.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Rutana.API.IAM.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Invitation endpoints")]
public class InvitationsController : ControllerBase
{
    private readonly IInvitationCommandService invitationCommandService;
    private readonly IInvitationQueryService invitationQueryService;
    private readonly IUserQueryService userQueryService;
    private readonly IInvitationRepository invitationRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IOrganizationQueryService organizationQueryService;

    public InvitationsController(
        IInvitationCommandService invitationCommandService,
        IInvitationQueryService invitationQueryService,
        IUserQueryService userQueryService,
        IInvitationRepository invitationRepository,
        IUnitOfWork unitOfWork,
        IOrganizationQueryService organizationQueryService)
    {
        this.invitationCommandService = invitationCommandService;
        this.invitationQueryService = invitationQueryService;
        this.userQueryService = userQueryService;
        this.invitationRepository = invitationRepository;
        this.unitOfWork = unitOfWork;
        this.organizationQueryService = organizationQueryService;
    }

    private User? GetAuthenticatedUser()
    {
        return HttpContext.Items["User"] as User;
    }

    private async Task<Suscriptions.Domain.Model.Aggregates.Organization?> GetOrganizationAsync(OrganizationId organizationId)
    {
        var query = new GetOrganizationByIdQuery(organizationId);
        return await organizationQueryService.Handle(query);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new invitation", Description = "Create a new invitation (Admin or Owner only)", OperationId = "CreateInvitation")]
    [SwaggerResponse(StatusCodes.Status201Created, "The invitation was created", typeof(InvitationResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
    public async Task<IActionResult> CreateInvitation([FromBody] CreateInvitationResource resource)
    {
        var authenticatedUser = GetAuthenticatedUser();
        if (authenticatedUser == null || authenticatedUser.OrganizationId == null)
        {
            return Unauthorized();
        }

        // Validar que el usuario tenga role Admin u Owner
        if (authenticatedUser.Role != UserRole.Admin && authenticatedUser.Role != UserRole.Owner)
        {
            return Forbid("Only Admin or Owner can create invitations");
        }

        try
        {
            var command = CreateInvitationCommandFromResourceAssembler.ToCommandFromResource(
                resource, 
                authenticatedUser.OrganizationId.Value);
            
            var invitation = await invitationCommandService.Handle(command);
            var organization = await GetOrganizationAsync(invitation.OrganizationId);
            
            // Obtener información del usuario invitado
            var getUserByIdQuery = new GetUserByIdQuery(invitation.UserId);
            var invitedUser = await userQueryService.Handle(getUserByIdQuery);
            
            var invitationResource = InvitationResourceFromEntityAssembler.ToResourceFromEntity(invitation, organization, invitedUser);
            
            return CreatedAtAction(nameof(GetInvitationById), new { id = invitation.Id }, invitationResource);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get user invitations", Description = "Get all pending invitations for the authenticated user", OperationId = "GetUserInvitations")]
    [SwaggerResponse(StatusCodes.Status200OK, "The invitations were found", typeof(IEnumerable<InvitationSummaryResource>))]
    public async Task<IActionResult> GetUserInvitations()
    {
        var authenticatedUser = GetAuthenticatedUser();
        if (authenticatedUser == null)
        {
            return Unauthorized();
        }

        var userQuery = new GetInvitationsByUserIdQuery(authenticatedUser.Id);
        var userInvitations = await invitationQueryService.Handle(userQuery);
        
        // Obtener organizaciones para cada invitación
        var userInvitationResources = new List<InvitationSummaryResource>();
        foreach (var invitation in userInvitations)
        {
            var organization = await GetOrganizationAsync(invitation.OrganizationId);
            userInvitationResources.Add(
                InvitationResourceFromEntityAssembler.ToSummaryResourceFromEntity(invitation, organization));
        }
        
        return Ok(userInvitationResources);
    }

    [HttpGet("organization")]
    [SwaggerOperation(Summary = "Get organization invitations", Description = "Get all pending invitations sent by the authenticated user's organization (Admin or Owner only)", OperationId = "GetOrganizationInvitations")]
    [SwaggerResponse(StatusCodes.Status200OK, "The invitations were found", typeof(IEnumerable<InvitationResource>))]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Only Admin or Owner can view organization invitations")]
    public async Task<IActionResult> GetOrganizationInvitations()
    {
        var authenticatedUser = GetAuthenticatedUser();
        if (authenticatedUser == null || authenticatedUser.OrganizationId == null)
        {
            return Unauthorized();
        }

        // Validar que el usuario tenga role Admin u Owner
        if (authenticatedUser.Role != UserRole.Admin && authenticatedUser.Role != UserRole.Owner)
        {
            return Forbid("Only Admin or Owner can view organization invitations");
        }

        var orgQuery = new GetInvitationsByOrganizationIdQuery(authenticatedUser.OrganizationId.Value);
        var orgInvitations = await invitationQueryService.Handle(orgQuery);
        
        // Obtener la organización una vez (todas las invitaciones son de la misma organización)
        var organization = await GetOrganizationAsync(authenticatedUser.OrganizationId);
        
        // Obtener información de usuarios para cada invitación
        var orgInvitationResources = new List<InvitationResource>();
        foreach (var invitation in orgInvitations)
        {
            var getUserByIdQuery = new GetUserByIdQuery(invitation.UserId);
            var invitedUser = await userQueryService.Handle(getUserByIdQuery);
            orgInvitationResources.Add(
                InvitationResourceFromEntityAssembler.ToResourceFromEntity(invitation, organization, invitedUser));
        }
        
        return Ok(orgInvitationResources);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get invitation by id", Description = "Get an invitation by its identifier", OperationId = "GetInvitationById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The invitation was found", typeof(InvitationResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The invitation was not found")]
    public async Task<IActionResult> GetInvitationById(int id)
    {
        var query = new GetInvitationByIdQuery(id);
        var invitation = await invitationQueryService.Handle(query);
        if (invitation == null) return NotFound();
        
        var authenticatedUser = GetAuthenticatedUser();
        if (authenticatedUser == null || invitation.UserId != authenticatedUser.Id)
        {
            return Forbid("You can only view your own invitations");
        }

        var organization = await GetOrganizationAsync(invitation.OrganizationId);
        
        // Obtener información del usuario invitado
        var getUserByIdQuery = new GetUserByIdQuery(invitation.UserId);
        var invitedUser = await userQueryService.Handle(getUserByIdQuery);
        
        var invitationResource = InvitationResourceFromEntityAssembler.ToResourceFromEntity(invitation, organization, invitedUser);
        return Ok(invitationResource);
    }

    [HttpPost("{id}/accept")]
    [SwaggerOperation(Summary = "Accept an invitation", Description = "Accept an invitation - Only users with NotAssigned role can accept", OperationId = "AcceptInvitation")]
    [SwaggerResponse(StatusCodes.Status200OK, "The invitation was accepted", typeof(InvitationResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The invitation was not found")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Only users with NotAssigned role can accept invitations")]
    public async Task<IActionResult> AcceptInvitation(int id)
    {
        var authenticatedUser = GetAuthenticatedUser();
        if (authenticatedUser == null)
        {
            return Unauthorized();
        }

        // Validar que el usuario tenga role NotAssigned
        if (authenticatedUser.Role != UserRole.NotAssigned)
        {
            return Forbid("Only users with NotAssigned role can accept invitations");
        }

        var invitation = await invitationRepository.FindByIdAsync(id);
        if (invitation == null)
        {
            return NotFound();
        }

        // Validar que el usuario autenticado es el dueño de la invitación
        if (invitation.UserId != authenticatedUser.Id)
        {
            return Forbid("You can only accept your own invitations");
        }

        try
        {
            var command = new AcceptInvitationCommand(id);
            var acceptedInvitation = await invitationCommandService.Handle(command);
            var organization = await GetOrganizationAsync(acceptedInvitation.OrganizationId);
            
            // Obtener información del usuario invitado
            var getUserByIdQuery = new GetUserByIdQuery(acceptedInvitation.UserId);
            var invitedUser = await userQueryService.Handle(getUserByIdQuery);
            
            var invitationResource = InvitationResourceFromEntityAssembler.ToResourceFromEntity(acceptedInvitation, organization, invitedUser);
            
            return Ok(invitationResource);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpDelete("{id}/cancel")]
    [SwaggerOperation(Summary = "Cancel an invitation", Description = "Cancel an invitation (delete it) - Anyone can cancel invitations", OperationId = "CancelInvitation")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The invitation was cancelled")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The invitation was not found")]
    public async Task<IActionResult> CancelInvitation(int id)
    {
        var authenticatedUser = GetAuthenticatedUser();
        if (authenticatedUser == null)
        {
            return Unauthorized();
        }

        var invitation = await invitationRepository.FindByIdAsync(id);
        if (invitation == null)
        {
            return NotFound();
        }

        try
        {
            invitationRepository.Remove(invitation);
            await unitOfWork.CompleteAsync();
            
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}

