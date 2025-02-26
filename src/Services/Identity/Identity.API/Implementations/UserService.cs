using Identity.API.Interfaces;

namespace Identity.API.Implementations;

public class UserService : IUserService
{
	private readonly DataContext _context;
	public UserService(DataContext context)
	{
		_context = context;
	}

	public async Task<bool> CheckValidEmail(string? email)
	{
		if (email is null)
		{
			throw new ApplicationException("Email is required");
		}
		var user = await _context.Users.FirstOrDefaultAsync(s => s.Email == email);
		if (user != null)
		{
			throw new ApplicationException("Email is already in use");
		}
		return true;
	}

	public async Task<User> FindValidUser(Guid? Id)
	{
		if (Id is null)
		{
			throw new ApplicationException("Id is required");
		}

		var user = await _context.Users.Include(s => s.Status)
								 .Include(s => s.Role)
								 .FirstOrDefaultAsync(s => s.Id == Id);

		if (user is null)
		{
			throw new ApplicationException("Email or password are not correct");
		}

		if (user.IsEmailConfirmed == false)
		{
			throw new ApplicationException("Account not verified");
		}

		if (user.StatusId == UserStatusConstant.BANNED)
		{
			throw new ApplicationException("Account was banned");
		}

		return user;
	}
}
