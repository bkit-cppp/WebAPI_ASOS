namespace Identity.API.Data.Seeding;

public class DataContextInitializer : IDataContextInitializer
{
	private readonly DataContext _context;
	public DataContextInitializer(DataContext context)
	{
		_context = context;
	}
	public async Task<int> InitRole()
	{
		int rows = 0;
		if (!_context.Roles.Any())
		{
			var admin = new Role()
			{
				Id = RoleConstant.ADMIN,
				Name = "Admin",
				Description = "Quản trị viên"
			};
			var customer = new Role()
			{
				Id = RoleConstant.CUSTOMER,
				Name = "Khách hàng",
				Description = "Khách hàng"
			};
			_context.Roles.Add(admin);
			_context.Roles.Add(customer);
			rows = await _context.SaveChangesAsync();
		}
		return rows;
	}

	public async Task<int> InitStatus()
	{
		int rows = 0;
		if (!_context.Statuses.Any())
		{
			var active = new Status()
			{
				Id = UserStatusConstant.ACTIVE,
				Name = "Hoạt động",
				Description = "Hoạt động"
			};
			var banned = new Status()
			{
				Id = UserStatusConstant.BANNED,
				Name = "Đã bị khóa",
				Description = "Đã bị khóa"
			};
			_context.Statuses.Add(active);
			_context.Statuses.Add(banned);
			rows = await _context.SaveChangesAsync();
		}
		return rows;
	}

	public async Task<int> InitUser()
	{
		int rows = 0;
		if (!_context.Users.Any())
		{
			string address = "273 Đ. An Dương Vương, Phường 3, Quận 5, Hồ Chí Minh , Việt Nam";
			var admin = new User()
			{
				Email = "admin",
				Fullname = "admin",
				Avatar = AvatarConstant.Default,
				Address = address,
				Password = "123456",
				Phone = "0123456789",
				IsEmailConfirmed = true,
				RoleId = RoleConstant.ADMIN,
				StatusId = UserStatusConstant.ACTIVE
			};
			_context.Users.Add(admin);

			for(int i = 1; i <= 10; i++)
			{
				var customer = new User()
				{
					Email = $"customer{i}@gmail.com",
					Fullname = $"customer{i}",
					Avatar = AvatarConstant.Default,
					Address = address,
					Password = "123456",
					Phone = "0123456789",
					IsEmailConfirmed = true,
					RoleId = RoleConstant.CUSTOMER,
					StatusId = UserStatusConstant.ACTIVE
				};
				_context.Users.Add(customer);
			}
			rows = await _context.SaveChangesAsync();
		}
		return rows;
	}

	public async Task SeedAsync()
	{
		try
		{
			await InitStatus();
			await InitRole();
			await InitUser();
		}
		catch(Exception ex) { }
	}
}
