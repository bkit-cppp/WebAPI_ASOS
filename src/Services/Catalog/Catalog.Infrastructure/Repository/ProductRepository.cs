using BuildingBlock.Caching.Services;

namespace Catalog.Infrastructure.Repository;

public class ProductRepository : GenericRepository<Product, Guid>, IProductRepository
{
	public ProductRepository(DataContext context) : base(context)
	{
		_context = context;
	}

    public async Task<List<Product>> GetNewestProductAsync()
    {
        var product = await _context.Products.Where(p => p.CreatedDate.HasValue).Take(2).ToListAsync();
        return product;
    }

    public async Task<List<Product>> GetTopRatedProductAsync(int topRate)
    {
        var product = await _context.Products.Where(p => p.AverageRating.Equals(topRate)).ToListAsync();
        return product;
    }
}
