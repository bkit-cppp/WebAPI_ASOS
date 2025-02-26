namespace Identity.API.Interfaces;

public interface IUserService
{
	Task<User> FindValidUser(Guid? Id);
	Task<bool> CheckValidEmail(string? email);
}
