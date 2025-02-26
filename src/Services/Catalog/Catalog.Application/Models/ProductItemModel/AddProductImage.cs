namespace Catalog.Application.Models.ProductItemModel;

public class AddProductImage
{
	public Guid ProductItemId { get; set; }
	public List<string> Urls { set; get; }
}
