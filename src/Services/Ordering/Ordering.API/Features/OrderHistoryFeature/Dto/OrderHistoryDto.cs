namespace Ordering.API.Features.OrderHistoryFeature.Dto;

public class OrderHistoryDto
{
	public Guid Id { get; set; }
	public Guid OrderId { get; set; }
	public string FromStatus { get; set; }
	public string ToStatus { get; set; }	
	public DateTime? CreatedDate { get; set; }
	private class Mapping : Profile
	{
		public Mapping()
		{
			CreateMap<OrderHistory, OrderHistoryDto>();
		}
	}
}
