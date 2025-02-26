namespace Catalog.Application.Models.ProductItemModel;

public class ProductItemPaginationRequest : PaginationRequest
{
	public Guid? ProductId { get; set; }
}
