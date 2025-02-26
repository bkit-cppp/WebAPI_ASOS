namespace Catalog.Application.Models.ProductModel;

public class ProductAddOrUpdate : AddOrUpdateRequest
{
	public Guid? Id { get; set; }
	public string Slug { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string? SizeAndFit { get; set; } = string.Empty;
	public string? Image { get; set; } = string.Empty;
	public decimal OriginalPrice { get; set; } = 0;
	public decimal SalePrice { get; set; } = 0;
	public bool IsSale { get; set; } = false;
	public Guid? BrandId { get; set; }
	public Guid? CategoryId { get; set; }
	public Guid? GenderId { get; set; }
}
