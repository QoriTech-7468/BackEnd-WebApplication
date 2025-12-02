using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Rutana.API.Suscriptions.Domain.Model.Queries;
using Rutana.API.Suscriptions.Domain.Services;
using Rutana.API.Suscriptions.Interfaces.REST.Resources;
using Rutana.API.Suscriptions.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Rutana.API.Suscriptions.Interfaces.REST;

/// <summary>
/// Controller for managing payments.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Payment Endpoints.")]
public class PaymentsController(IPaymentQueryService paymentQueryService)
    : ControllerBase
{
    /// <summary>
    /// Gets a payment by its unique identifier.
    /// </summary>
    /// <param name="paymentId">The unique identifier of the payment.</param>
    /// <returns>An <see cref="IActionResult"/> containing the payment resource if found, or NotFound if not.</returns>
    [HttpGet("{paymentId:int}")]
    [SwaggerOperation("Get Payment by Id", "Get a payment by its unique identifier.", OperationId = "GetPaymentById")]
    [SwaggerResponse(200, "The payment was found and returned.", typeof(PaymentResource))]
    [SwaggerResponse(404, "The payment was not found.")]
    public async Task<IActionResult> GetPaymentById(int paymentId)
    {
        var getPaymentByIdQuery = new GetPaymentByIdQuery(paymentId);
        
        var payment = await paymentQueryService.Handle(getPaymentByIdQuery);

        if (payment is null) return NotFound();

        var paymentResource = PaymentResourceFromEntityAssembler.ToResourceFromEntity(payment);

        return Ok(paymentResource);
    }
}