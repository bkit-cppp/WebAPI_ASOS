using BuildingBlock.Caching.Services;
using Catalog.Infrastructure.Repository;

namespace Catalog.Infrastructure.Seedworks;

public class UnitOfWork : IUnitOfWork, IDisposable
{
	private readonly DataContext _context;
	private readonly ICacheService _cache;

	public IGenderRepository Genders { get; private set; }
	public IColorRepository Colors { get; private set; }
	public IBrandRepository Brands { get; private set; }
	public ICategoryRepository Categories { get; private set; }
	public ICommentRepository Comments { get; private set; }
	public IImageRepository Images { get; private set; }
	public IProductItemRepository ProductItems { get; private set; }
	public IProductRepository Products { get; private set; }
	public IRatingRepository Ratings { get; private set; }
	public ISizeCategoryRepository SizeCategories { get; private set; }
	public ISizeRepository Sizes { get; private set; }
	public IUserCommentRepository UserComments { get; private set; }
	public IVariationRepository Variations { get; private set; }
	public IWishlistRepository Wishlists { get; private set; }
	public ICategoryGenderRepository CategoryGenders { get; private set; }

	public UnitOfWork(
		DataContext context,
		ICacheService cache)
	{
		_context = context;
		_cache = cache;
		Genders = new GenderRepository(_context);
		Colors = new ColorRepository(_context);
		Brands = new BrandRepository(_context);
		Categories = new CategoryRepository(_context);
		Comments = new CommentRepository(_context);
		Images = new ImageRepository(_context);
		ProductItems = new ProductItemRepository(_context);
		Products = new ProductRepository(_context);
		Ratings = new RatingRepository(_context);
		SizeCategories = new SizeCategoryRepository(_context);
		Sizes = new SizeRepository(_context);
		UserComments = new UserCommentRepository(_context);
		Variations = new VariationRepository(_context);
		Wishlists = new WishlistRepository(_context);
		CategoryGenders = new CategoryGenderRepository(_context);
	}

	public async Task<int> CompleteAsync()
	{
		return await _context.SaveChangesAsync();
	}

	public void Dispose()
	{
		_context.Dispose();
	}

	public async Task RemoveCacheAsync(string key)
	{
		try
		{
			await _cache.RemoveCacheResponseAsync(key);
		}
		catch(Exception ex)
		{
			Console.WriteLine($"[RemoveCacheAsync] - failed - error: {ex.Message}");
		}
	}
}