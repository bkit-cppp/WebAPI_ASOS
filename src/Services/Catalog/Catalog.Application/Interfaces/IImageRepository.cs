namespace Catalog.Application.Interfaces;

public interface IImageRepository : IGenericRepository<Image, Guid>
{
    Task<List<Image>> GetImagesByProductItemIdAsync(Guid productItemId);
}
