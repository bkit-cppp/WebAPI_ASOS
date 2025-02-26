using System.Reflection;

namespace Promotion.API.Data;

public class DataContext : DbContext
{
	public DbSet<Discount> Discounts => Set<Discount>();
	public DbSet<DiscountType> DiscountTypes => Set<DiscountType>();
	public DbSet<DiscountHistory> DiscountHistories => Set<DiscountHistory>();
	public DbSet<DiscountProduct> DiscountProducts => Set<DiscountProduct>();
	public DataContext(DbContextOptions<DataContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}

}
