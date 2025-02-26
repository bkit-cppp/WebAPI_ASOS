using static MassTransit.Logging.OperationName;

namespace Catalog.Application.Interfaces;

public interface IProductRepository : IGenericRepository<Product, Guid>
{
    Task<List<Product>> GetTopRatedProductAsync(int topRate);
    Task<List<Product>> GetNewestProductAsync();
}
