using Rutana.API.CRM.Domain.Model.Commands;
using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.CRM.Interfaces.REST.Resources;

namespace Rutana.API.CRM.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert UpdateLocationResource to UpdateLocationCommand.
/// </summary>
public static class UpdateLocationCommandFromResourceAssembler
{
    /// <summary>
    /// Convert resource to command.
    /// </summary>
    /// <param name="resource">The update location resource.</param>
    /// <returns>The update location command.</returns>
    public static UpdateLocationCommand ToCommandFromResource(UpdateLocationResource resource)
    {
        var locationIdVo = new LocationId(resource.Id);
        
        // Parse the proximity string to enum (handle lowercase: close, mid, far)
        var proximityString = resource.Proximity.ToLowerInvariant();
        Proximity proximity = proximityString switch
        {
            "close" => Proximity.Close,
            "mid" => Proximity.Mid,
            "far" => Proximity.Far,
            _ => throw new ArgumentException($"Invalid proximity value: {resource.Proximity}. Valid values are: close, mid, far")
        };

        return new UpdateLocationCommand(
            locationIdVo,
            resource.Address,
            resource.Latitude,
            resource.Longitude,
            proximity,
            resource.IsActive,
            resource.ClientId);
    }
}

