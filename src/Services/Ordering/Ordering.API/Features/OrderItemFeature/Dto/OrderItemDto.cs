namespace Ordering.API.Features.OrderItemFeature.Dto;

public class OrderItemDto
{
	public Guid Id { get; set; }
	public Guid OrderId { get; set; }
	public Guid ProductId { get; set; }
	public Guid ProductItemId { get; set; }
	public Guid VariationId { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Brand { get; set; } = string.Empty;
	public string Category { get; set; } = string.Empty;
	public string Size { get; set; } = string.Empty;
	public string Color { get; set; } = string.Empty;
	public string Image { get; set; } = string.Empty;
	public decimal Price { get; set; } = 0;
	public decimal Stock { get; set; } = 0;
	public int Quantity { get; set; } = 0;
	public decimal Amount { get; set; } = 0;
	private class Mapping : Profile
	{
		public Mapping()
		{
			CreateMap<OrderItem, OrderItemDto>();
		}
	}
}
