namespace Promotion.API.Features.DiscountHistoryFeature.Dto;

public class DiscountHistoryDto
{
    public Guid Id { get; set; }
    public decimal DiscountApplied { get; set; }
    public Guid? DiscountId { get; set; }
	public DateTime? CreatedDate { get; set; }
	private class Mapping : Profile
	{
		public Mapping()
		{
			CreateMap<DiscountHistory, DiscountHistoryDto>();
		}
	}
}
