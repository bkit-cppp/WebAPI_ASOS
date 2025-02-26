using Catalog.Application.Interfaces;

namespace Catalog.Application.Commons.Interfaces;

public interface IUnitOfWork
{
	IGenderRepository Genders { get; }
	IColorRepository Colors { get; }
	IBrandRepository Brands { get; }
	ICategoryRepository Categories { get; }
	ICommentRepository Comments { get; }
	IImageRepository Images { get; }
	IProductItemRepository ProductItems { get; }
	IProductRepository Products { get; }
	IRatingRepository Ratings { get; }
	ISizeCategoryRepository SizeCategories { get; }
	ISizeRepository Sizes { get; }
	IUserCommentRepository UserComments { get; }
	IVariationRepository Variations { get; }
	IWishlistRepository Wishlists { get; }
	ICategoryGenderRepository CategoryGenders { get; }

	Task RemoveCacheAsync(string key);
	Task<int> CompleteAsync();
	void Dispose();
}
