namespace Rutana.API.IAM.Domain.Model.Commands;

public record SignUpCommand(string Name, string Surname, string Phone, string Email, string Password);