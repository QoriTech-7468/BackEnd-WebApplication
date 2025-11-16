namespace Rutana.API.Fleet.Domain.Model.ValueObjects;

/// <summary>
/// Represents the state of a vehicle.
/// </summary>
public enum VehicleState
{
    /// <summary>
    /// Vehicle is enabled and available for routes.
    /// </summary>
    Enabled,
    
    /// <summary>
    /// Vehicle is disabled and not available for routes.
    /// </summary>
    Disabled
}