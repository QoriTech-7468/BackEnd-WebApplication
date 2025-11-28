using Rutana.API.IAM.Domain.Model.Enums;

namespace Rutana.API.IAM.Domain.Model.Commands;

// Agregamos Role y OrganizationId
public record SignUpCommand(
    string Name, 
    string Surname, 
    string Phone, 
    string Email, 
    string Password, 
    UserRole Role, 
    int? OrganizationId
);