namespace Rutana.API.IAM.Interfaces.REST.Resources;

public record CreateUserResource(
    string Name,
    string Surname,
    string Phone,
    string Email,
    string Password,
    string Role,
    int OrganizationId
);