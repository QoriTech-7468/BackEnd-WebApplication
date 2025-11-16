using Rutana.API.CRM.Domain.Model.Commands;
using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.CRM.Interfaces.REST.Resources;

namespace Rutana.API.CRM.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert RegisterLocationResource to RegisterLocationCommand.
/// </summary>
public static class RegisterLocationCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a RegisterLocationResource to a RegisterLocationCommand.
    /// </summary>
    /// <param name="resource">The RegisterLocationResource.</param>
    /// <returns>The RegisterLocationCommand.</returns>
    public static RegisterLocationCommand ToCommandFromResource(RegisterLocationResource resource)
    {
        // Parse the proximity string to enum
        if (!Enum.TryParse<Proximity>(resource.Proximity, true, out var proximity))
            throw new ArgumentException($"Invalid proximity value: {resource.Proximity}");

        return new RegisterLocationCommand(
            resource.ClientId,
            resource.Name,
            proximity);
    }
}