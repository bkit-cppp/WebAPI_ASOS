namespace Promotion.API.Features.DiscountTypeFeature.Dto;

public class DiscountTypeDto
{
    public string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
	private class Mapping : Profile
	{
		public Mapping()
		{
			CreateMap<DiscountType, DiscountTypeDto>();
		}
	}
}
