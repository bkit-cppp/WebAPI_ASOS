namespace Catalog.Application.Models.ProductModel;

public class ProductPaginationRequest : PaginationRequest
{
	public Guid? GenderId { get; set; }
	public Guid? CategoryId { get; set; }
	public Guid? BrandId { get; set; }
}
