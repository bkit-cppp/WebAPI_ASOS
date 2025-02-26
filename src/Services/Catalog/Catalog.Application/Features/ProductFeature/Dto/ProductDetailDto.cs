using Catalog.Application.Features.ProductItemFeature.Dto;

namespace Catalog.Application.Features.ProductFeature.Dto;
public class ProductDetailDto
{
	public Guid Id { get; set; }
	public string Slug { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public decimal AverageRating { get; set; } = 0;
	public string? SizeAndFit { get; set; } = string.Empty;
	public string? Image { get; set; } = string.Empty;
	public decimal OriginalPrice { get; set; } = 0;
	public decimal SalePrice { get; set; } = 0;
	public bool IsSale { get; set; } = false;
	public string? Category { get; set; }
	public string? Brand { get; set; }
	public ProductDetailAction Action { get; set; } = new ProductDetailAction();
	public List<ProductItemDetailDto> Items { get; set; }
}

public class ProductDetailAction
{
	public bool Bought { get; set; } = false;
	public bool AddWishlist { get; set; } = false;
	public bool Rated { get; set; } = false;
	public int PointRated { get; set; } = 0;
}