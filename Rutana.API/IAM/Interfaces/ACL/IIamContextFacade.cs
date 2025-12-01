namespace Rutana.API.IAM.Interfaces.ACL;

public interface IIamContextFacade
{
    Task<int> CreateUser(string name, string surname, string phone, string email, string password, string role, int organizationId);
    
    Task<int> FetchUserIdByUsername(string username);
    Task<string> FetchUsernameByUserId(int userId);
    
    Task UpdateUserOrganizationAndRole(int userId, int organizationId, string role);
}