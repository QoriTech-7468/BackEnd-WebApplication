namespace Rutana.API.IAM.Interfaces.REST.Resources;

public record UserResource(int Id, string Name, string Surname, string Phone, string Email, string Role, int? OrganizationId);