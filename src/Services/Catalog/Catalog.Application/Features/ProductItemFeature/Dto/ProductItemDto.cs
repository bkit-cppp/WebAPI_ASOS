using Catalog.Application.Commons.Models;
using Catalog.Application.Features.ColorFeature.Dto;

namespace Catalog.Application.Features.ProductItemFeature.Dto;

public class ProductItemDto : BaseDto<Guid>
{
	public Guid? ProductId { get; set; }
	public Guid? ColorId { get; set; }
	public decimal AdditionalPrice { get; set; } = 0;
	public List<ProductImageDto> Images { get; set;} = new List<ProductImageDto>();
	public ColorDto? Color { get; set; }
	private class Mapping : Profile
	{
		public Mapping()
		{
			CreateMap<ProductItem, ProductItemDto>()
				.ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
				.ForMember(dest => dest.Images, opt => 
					opt.MapFrom(src => src.Images != null ? src.Images.Select(s => new ProductImageDto { 
						Id = s.Id,
						Url = s.Url
					}) : null));
		}
	}

}
