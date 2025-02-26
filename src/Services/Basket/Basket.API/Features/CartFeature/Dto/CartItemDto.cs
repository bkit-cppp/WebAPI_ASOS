namespace Basket.API.Features.CartFeature.Dto;

public class CartItemDto
{
	public Guid ProductId { get; set; }
	public Guid ProductItemId { get; set; }
	public Guid VariationId { get; set; }
	public string Slug { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string Category { get; set; } = string.Empty;
	public string Brand { get; set; } = string.Empty;
	public string Size { get; set; } = string.Empty;
	public string Color { get; set; } = string.Empty;
	public decimal OriginalPrice { get; set; }
	public decimal SalePrice { get; set; }
	public decimal AdditionalPrice { get; set; }
	public decimal Stock { get; set; }
	public bool IsSale { get; set; }
	public int Quantity { get; set; }
	public decimal TotalPrice
	{
		get
		{
			return (OriginalPrice + AdditionalPrice + Stock) * Quantity;
		}
	}
	public decimal TotalSalePrice
	{
		get
		{
			return (SalePrice + AdditionalPrice + Stock) * Quantity;
		}
	}
	public decimal FinalTotal
	{
		get
		{
			return IsSale == true ? TotalSalePrice : TotalPrice;
		}
	}
	public string Image { get; set; } = string.Empty;
}
