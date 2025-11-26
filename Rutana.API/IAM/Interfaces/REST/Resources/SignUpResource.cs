namespace Rutana.API.IAM.Interfaces.REST.Resources;

public record SignUpResource(string Name, string Surname, string Phone, string Email, string Password, string Role, int OrganizationId);