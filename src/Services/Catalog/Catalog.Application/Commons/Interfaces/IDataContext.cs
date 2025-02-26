namespace Catalog.Application.Commons.Interfaces;

public interface IDataContext
{
	DbSet<Gender> Genders { get; }
	DbSet<Product> Products { get; }
	DbSet<Comment> Comments { get; }
	DbSet<UserComment> UserComments { get; }
	DbSet<Category> Categories { get; }
	DbSet<Rating> Ratings { get; }
	DbSet<Color> Colors { get; }
	DbSet<Image> Images { get; }
	DbSet<Size> Sizes { get; }
	DbSet<SizeCategory> SizeCategories { get; }
	DbSet<Variation> Variations { get; }
	DbSet<WishList> WishLists { get; }
	DbSet<ProductItem> ProductItems { get; }
	DbSet<Brand> Brands { get; }
	Task<int> SaveChangesAsync();
	Task<int> SaveChangesAsync(CancellationToken token);
}
