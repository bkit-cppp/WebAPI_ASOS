using System.Reflection;

namespace Catalog.Infrastructure.Data;

public class DataContext : DbContext , IDataContext
{
	public DataContext(DbContextOptions<DataContext> options) : base(options) { }

	public DbSet<Gender> Genders => Set<Gender>();
	public DbSet<Product> Products => Set<Product>();
	public DbSet<Comment> Comments => Set<Comment>();
	public DbSet<Category> Categories => Set<Category>();
	public DbSet<UserComment> UserComments => Set<UserComment>();
	public DbSet<Rating> Ratings => Set<Rating>();
	public DbSet<Color> Colors => Set<Color>();
	public DbSet<Image> Images => Set<Image>();
	public DbSet<Size> Sizes => Set<Size>();
	public DbSet<SizeCategory> SizeCategories => Set<SizeCategory>();
	public DbSet<Variation> Variations => Set<Variation>();
	public DbSet<WishList> WishLists => Set<WishList>();
	public DbSet<ProductItem> ProductItems => Set<ProductItem>();
	public DbSet<Brand> Brands => Set<Brand>();
	public DbSet<CategoryGender> CategoryGenders => Set<CategoryGender>();

	public Task<int> SaveChangesAsync()
	{
		return base.SaveChangesAsync();
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
