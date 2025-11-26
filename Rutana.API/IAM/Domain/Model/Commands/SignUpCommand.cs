namespace Rutana.API.IAM.Domain.Model.Commands;

// Agregamos Role y OrganizationId
public record SignUpCommand(
    string Name, 
    string Surname, 
    string Phone, 
    string Email, 
    string Password, 
    string Role, 
    int OrganizationId
);