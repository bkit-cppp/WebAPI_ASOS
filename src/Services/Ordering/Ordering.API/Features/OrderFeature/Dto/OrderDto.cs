using AutoMapper;
using Ordering.API.Features.OrderStatusFeature.Dto;

namespace Ordering.API.Features.OrderFeature.Dto;

public class OrderDto
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public Guid? DiscountId { get; set; }
	public Guid? TransactionId { get; set; }
	public decimal BasePrice { get; set; } = 0;
	public decimal DiscountPrice { get; set; } = 0;
	public decimal SubPrice { get; set; } = 0;
	public int PointUsed { get; set; } = 0;
	public decimal Total { get; set; } = 0;
	public string ReceiverName { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public string Address { get; set; }
	public DateTime CreatedDate { get; set; }
	public string? StatusId { get; set; }
	public OrderStatusDto? Status { get; set; }
	private class Mapping : Profile
	{
		public Mapping()
		{
			CreateMap<Order, OrderDto>()
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status ?? null));
		}
	}
}
