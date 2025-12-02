using Rutana.API.CRM.Domain.Model.ValueObjects;

namespace Rutana.API.CRM.Domain.Model.Commands;

/// <summary>
/// Command to update a client.
/// </summary>
/// <param name="ClientId">The client identifier.</param>
/// <param name="CompanyName">The company name of the client.</param>
/// <param name="IsEnabled">The enabled status of the client.</param>
public record UpdateClientCommand(
    ClientId ClientId,
    string CompanyName,
    bool IsEnabled);

