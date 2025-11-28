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
using Rutana.API.Shared.Domain.Repositories;
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

    public InvitationsController(
        IInvitationCommandService invitationCommandService,
        IInvitationQueryService invitationQueryService,
        IUserQueryService userQueryService,
        IInvitationRepository invitationRepository,
        IUnitOfWork unitOfWork)
    {
        this.invitationCommandService = invitationCommandService;
        this.invitationQueryService = invitationQueryService;
        this.userQueryService = userQueryService;
        this.invitationRepository = invitationRepository;
        this.unitOfWork = unitOfWork;
    }

    private User? GetAuthenticatedUser()
    {
        return HttpContext.Items["User"] as User;
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
            var invitationResource = InvitationResourceFromEntityAssembler.ToResourceFromEntity(invitation);
            
            return CreatedAtAction(nameof(GetInvitationById), new { id = invitation.Id }, invitationResource);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get invitations", Description = "Get invitations. Use 'scope' query parameter: 'user' for user's invitations (default) or 'organization' for organization's invitations (Admin/Owner only)", OperationId = "GetInvitations")]
    [SwaggerResponse(StatusCodes.Status200OK, "The invitations were found", typeof(IEnumerable<InvitationSummaryResource>))]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Only Admin or Owner can view organization invitations")]
    public async Task<IActionResult> GetInvitations([FromQuery] string? scope = "user")
    {
        var authenticatedUser = GetAuthenticatedUser();
        if (authenticatedUser == null)
        {
            return Unauthorized();
        }

        // Si el scope es "organization", retornar invitaciones de la organización
        if (scope?.ToLower() == "organization")
        {
            if (authenticatedUser.OrganizationId == null)
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
            var orgInvitationResources = orgInvitations.Select(InvitationResourceFromEntityAssembler.ToResourceFromEntity);
            
            return Ok(orgInvitationResources);
        }

        // Por defecto, retornar invitaciones del usuario
        var userQuery = new GetInvitationsByUserIdQuery(authenticatedUser.Id);
        var userInvitations = await invitationQueryService.Handle(userQuery);
        var userInvitationResources = userInvitations.Select(InvitationResourceFromEntityAssembler.ToSummaryResourceFromEntity);
        
        return Ok(userInvitationResources);
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

        var invitationResource = InvitationResourceFromEntityAssembler.ToResourceFromEntity(invitation);
        return Ok(invitationResource);
    }

    [HttpPost("{id}/accept")]
    [SwaggerOperation(Summary = "Accept an invitation", Description = "Accept an invitation", OperationId = "AcceptInvitation")]
    [SwaggerResponse(StatusCodes.Status200OK, "The invitation was accepted", typeof(InvitationResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The invitation was not found")]
    public async Task<IActionResult> AcceptInvitation(int id)
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

        // Validar que el usuario autenticado es el dueño de la invitación
        if (invitation.UserId != authenticatedUser.Id)
        {
            return Forbid("You can only accept your own invitations");
        }

        try
        {
            var command = new AcceptInvitationCommand(id);
            var acceptedInvitation = await invitationCommandService.Handle(command);
            var invitationResource = InvitationResourceFromEntityAssembler.ToResourceFromEntity(acceptedInvitation);
            
            return Ok(invitationResource);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Reject an invitation", Description = "Reject an invitation (delete it)", OperationId = "RejectInvitation")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The invitation was rejected")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The invitation was not found")]
    public async Task<IActionResult> RejectInvitation(int id)
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

        // Validar que el usuario autenticado es el dueño de la invitación
        if (invitation.UserId != authenticatedUser.Id)
        {
            return Forbid("You can only reject your own invitations");
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

