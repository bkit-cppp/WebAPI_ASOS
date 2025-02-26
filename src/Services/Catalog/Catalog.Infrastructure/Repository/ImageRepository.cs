namespace Catalog.Infrastructure.Repository;

public class ImageRepository : GenericRepository<Image, Guid>, IImageRepository
{
	public ImageRepository(DataContext context) : base(context)
	{
		_context = context;
	}
    public async Task<List<Image>> GetImagesByProductItemIdAsync(Guid productItemId)
    {
        return await _dbSet.Where(img => img.ProductItemId == productItemId).ToListAsync();
    }
}
