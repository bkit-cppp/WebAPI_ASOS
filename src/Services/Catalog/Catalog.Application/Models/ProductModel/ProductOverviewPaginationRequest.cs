namespace Catalog.Application.Models.ProductModel;

public class ProductOverviewPaginationRequest : PaginationRequest
{
	public string? Gender { get; set; }
	public string? Category { get; set; }
	public string? Brand { get; set; }
	public bool? Sale { get; set; }
	public string? Price { get; set; }
	public string? Sort { get; set; }
}
