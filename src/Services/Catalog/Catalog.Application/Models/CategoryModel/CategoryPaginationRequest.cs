namespace Catalog.Application.Models.CategoryModel;

public class CategoryPaginationRequest : PaginationRequest
{
	public Guid? ParentId { get; set; } = null;
}
