using Promotion.API.Features.DiscountTypeFeature.Dto;
using System.Text.Json.Serialization;

namespace Promotion.API.Features.DiscountFeature.Dto;
public class DiscountDto
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Code { get; set; } = string.Empty;
    public decimal Value { get; set; } = 0;
    public decimal Minimum { get; set; } = 0;
	public string Condition { get; set; } = string.Empty;
	public int Quantity { get; set; }
	public bool Available { get; set; } = false;
	public DiscountTypeDto? DiscountType { get; set; }
	public string? DiscountTypeId { get; set; }
	private class Mapping : Profile
	{
		public Mapping()
		{
			CreateMap<Discount, DiscountDto>()
				.ForMember(dest => dest.DiscountType, opt => opt.MapFrom(src => src.DiscountType ?? null));
		}
	}
}
