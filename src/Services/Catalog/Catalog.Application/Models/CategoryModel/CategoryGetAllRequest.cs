namespace Catalog.Application.Models.CategoryModel;

public class CategoryGetAllRequest : BaseRequest
{
	public Guid? ParentId { get; set; } = null;
}
