namespace Ordering.API.Features.OrderStatusFeature.Dto;

public class OrderStatusDto
{
	public string Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	private class Mapping : Profile
	{
		public Mapping()
		{
			CreateMap<OrderStatus, OrderStatusDto>();
		}
	}
}
