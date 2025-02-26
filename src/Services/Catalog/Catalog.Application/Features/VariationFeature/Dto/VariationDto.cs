using Catalog.Application.Commons.Models;
using Catalog.Application.Features.SizeFeature.Dto;

namespace Catalog.Application.Features.VariationFeature.Dto;

public class VariationDto : BaseDto<Guid>
{
    public Guid Id { get; set; }
    public int QtyDisplay { get; set; } = 0;
    public int QtyInStock { get; set; } = 0;
    public decimal Stock { get; set; } = 0;
	public Guid? ProductItemId { get; set; }
	public Guid? SizeId { get; set; }
    public SizeDto Size { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Variation, VariationDto>()
				.ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size));
        }
    }
}
