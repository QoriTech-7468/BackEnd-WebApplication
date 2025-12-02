using Microsoft.AspNetCore.Mvc;
using Rutana.API.Suscriptions.Domain.Model.Queries;
using Rutana.API.Suscriptions.Domain.Model.ValueObjects;
using Rutana.API.Suscriptions.Domain.Services;
using Rutana.API.Suscriptions.Interfaces.REST.Resources;
using Rutana.API.Suscriptions.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Rutana.API.Suscriptions.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class SubscriptionsController(
    ISubscriptionCommandService commandService, 
    ISubscriptionQueryService queryService) 
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(Summary = "Create Subscription")]
    public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionResource resource)
    {
        var command = CreateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var subscription = await commandService.Handle(command);
        var subscriptionResource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        
        return CreatedAtAction(nameof(GetSubscriptionById), new { id = subscriptionResource.Id }, subscriptionResource);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get Subscription by Id")]
    public async Task<IActionResult> GetSubscriptionById(int id)
    {
        var subscriptionIdObject = new SubscriptionId(id);
        var query = new GetSubscriptionByIdQuery(subscriptionIdObject);
        var subscription = await queryService.Handle(query);
        
        if (subscription == null) return NotFound();
        
        var resource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        return Ok(resource);
    }
}