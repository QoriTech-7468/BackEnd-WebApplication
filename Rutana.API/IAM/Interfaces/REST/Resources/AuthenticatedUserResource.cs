namespace Rutana.API.IAM.Interfaces.REST.Resources;

public record AuthenticatedUserResource(
    int Id, 
    string Name, 
    string Surname, 
    string Token, 
    int? OrganizationId, 
    string Role
);