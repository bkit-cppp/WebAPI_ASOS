using BuildingBlock.Constants;

namespace Basket.API.Features.CartFeature.Dto;

public class CartDto
{
	public Guid UserId { get; set; }
	public DiscountDto? Discount { get; set; }
	public decimal BasePrice { get; set; } = 0;
	public decimal DiscountPrice { get; set; } = 0;
	public decimal SubPrice { get; set; } = 0;
	public int PointUsed { get; set; } = 0;
	public decimal Total { get; set; } = 0;
	public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();

	public CartDto(Guid id)
	{
		UserId = id;
		Discount = null;
		SubPrice = 5;
		Items = new List<CartItemDto>();
	}

	public void ProcessData()
	{
		if (Items.Any())
		{
			BasePrice = Items.Sum(s => s.IsSale == true ? s.TotalSalePrice : s.TotalPrice);
		}
		else
		{
			BasePrice = 0;
			Discount = null;
			DiscountPrice = 0;
			PointUsed = 0;
		}

		BasePrice = Math.Max(0, BasePrice);

		if (Discount != null)
		{
			string discountTypeId = Discount.DiscountTypeId;
			if (discountTypeId == DiscountTypeConstant.Money || discountTypeId == DiscountTypeConstant.Product)
			{
				DiscountPrice = Discount.Value;
			}
			else if (discountTypeId == DiscountTypeConstant.Percentage)
			{
				DiscountPrice = Math.Round(((BasePrice - PointUsed) * Discount.Value) / 100, 1);
			}
			else
			{
				DiscountPrice = 0;
			}
		}
		else
		{
			DiscountPrice = 0;
		}

		DiscountPrice = Math.Max(0, DiscountPrice);

		Total = BasePrice + SubPrice - PointUsed - DiscountPrice;

		Total = Math.Max(0, Total);
	}

	public void FakeData(Guid id)
	{
		UserId = id;
		Discount = new DiscountDto
		{
			DiscountTypeId = DiscountTypeConstant.Percentage,
			Value = 10
		};
		BasePrice = 1000;
		SubPrice = 50;
		PointUsed = 100;
		Items = new List<CartItemDto>
		{
			new CartItemDto
			{
				ProductId = Guid.NewGuid(),
				ProductItemId = Guid.NewGuid(),
				VariationId = Guid.NewGuid(),
				Slug = "product-1",
				Name = "Product 1",
				Description = "This is the first product",
				Category = "Electronics",
				Brand = "Brand A",
				Size = "M",
				Color = "Red",
				OriginalPrice = 500,
				SalePrice = 450,
				Stock = 20,
				IsSale = true,
				Quantity = 2,
				Image = "product1.jpg"
			},
			new CartItemDto
			{
				ProductId = Guid.NewGuid(),
				ProductItemId = Guid.NewGuid(),
				VariationId = Guid.NewGuid(),
				Slug = "product-2",
				Name = "Product 2",
				Description = "This is the second product",
				Category = "Books",
				Brand = "Brand B",
				Size = "L",
				Color = "Blue",
				OriginalPrice = 300,
				SalePrice = 250,
				Stock = 10,
				IsSale = false,
				Quantity = 1,
				Image = "product2.jpg"
			}
		};
	}
}
