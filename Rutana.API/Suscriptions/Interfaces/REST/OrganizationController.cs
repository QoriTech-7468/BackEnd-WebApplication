using System.Net.Mime;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Suscriptions.Domain.Model.Queries;
using Rutana.API.Suscriptions.Domain.Services;
using Rutana.API.Suscriptions.Interfaces.REST.Resources;
using Rutana.API.Suscriptions.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Rutana.API.Suscriptions.Interfaces.REST;

/// <summary>
/// Controller for managing organizations.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Organization Endpoints.")]
public class OrganizationController(
    IOrganizationCommandService organizationCommandService,
    IOrganizationQueryService organizationQueryService)
    : ControllerBase
{
    /// <summary>
    /// Gets an organization by its unique identifier.
    /// </summary>
    /// <param name="organizationId">The unique identifier of the organization.</param>
    /// <returns>An <see cref="IActionResult"/> containing the organization resource if found, or NotFound if not.</returns>
    [HttpGet("{organizationId:int}")]
    [SwaggerOperation("Get Organization by Id", "Get an organization by its unique identifier.", OperationId = "GetOrganizationById")]
    [SwaggerResponse(200, "The organization was found and returned.", typeof(OrganizationResource))]
    [SwaggerResponse(404, "The organization was not found.")]
    public async Task<IActionResult> GetOrganizationById(int organizationId)
    {
        var organizationIdValueObject = new OrganizationId(organizationId);
        var getOrganizationByIdQuery = new GetOrganizationByIdQuery(organizationIdValueObject);
        var organization = await organizationQueryService.Handle(getOrganizationByIdQuery);

        if (organization is null) return NotFound();

        var organizationResource = OrganizationResourceFromEntityAssembler.ToResourceFromEntity(organization);

        return Ok(organizationResource);
    }

    /// <summary>
    /// Creates a new organization.
    /// </summary>
    /// <param name="resource">The resource containing the organization data to create.</param>
    /// <returns>An <see cref="IActionResult"/> containing the created organization resource, or BadRequest if creation failed.</returns>
    [HttpPost]
    [SwaggerOperation("Create Organization", "Create a new organization.", OperationId = "CreateOrganization")]
    [SwaggerResponse(201, "The organization was created.", typeof(OrganizationResource))]
    [SwaggerResponse(400, "The organization was not created.")]
    public async Task<IActionResult> CreateOrganization(CreateOrganizationResource resource)
    {
        var createOrganizationCommand = CreateOrganizationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var organization = await organizationCommandService.Handle(createOrganizationCommand);

        if (organization is null) return BadRequest();

        var organizationResource = OrganizationResourceFromEntityAssembler.ToResourceFromEntity(organization);

        return CreatedAtAction(nameof(GetOrganizationById), new { organizationId = organization.Id.Value }, organizationResource);
    }
}

