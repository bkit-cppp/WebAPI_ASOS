using System.Reflection;

namespace Identity.API.Data;

public class DataContext : DbContext
{
	public DbSet<HubConnection> HubConnections => Set<HubConnection>();
	public DbSet<Token> Tokens => Set<Token>();
	public DbSet<User> Users => Set<User>();
	public DbSet<Role> Roles => Set<Role>();
	public DbSet<Status> Statuses => Set<Status>();
	public DbSet<OTP> OTPs => Set<OTP>();
	public DbSet<PointHistory> PointHistories => Set<PointHistory>();
	public DbSet<Notification> Notifications => Set<Notification>();
	public DataContext(DbContextOptions<DataContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}