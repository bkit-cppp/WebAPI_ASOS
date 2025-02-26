namespace Promotion.API.Features.DiscountProductFeature.Dto;

public class DiscountProductDto
{
    public Guid Id { get; set; }
    public Guid? DiscountId { get; set; }
    public Guid? ProductId { get; set; }
	private class Mapping : Profile
	{
		public Mapping()
		{
			CreateMap<DiscountProduct, DiscountProductDto>();
		}
	}
}
