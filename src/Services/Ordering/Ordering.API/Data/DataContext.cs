using System.Reflection;

namespace Ordering.API.Data;

public class DataContext : DbContext
{
	public DbSet<Order> Orders => Set<Order>();
	public DbSet<OrderItem> OrderItems => Set<OrderItem>();
	public DbSet<OrderStatus> OrderStatus => Set<OrderStatus>();
	public DbSet<OrderHistory> OrderHistories => Set<OrderHistory>();
	public DbSet<Transaction> Transactions => Set<Transaction>();
	public DbSet<Refund> Refunds => Set<Refund>();
	public DataContext(DbContextOptions<DataContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}