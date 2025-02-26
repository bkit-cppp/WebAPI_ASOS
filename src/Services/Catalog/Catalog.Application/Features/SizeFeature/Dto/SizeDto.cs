using Catalog.Application.Commons.Models;

namespace Catalog.Application.Features.SizeFeature.Dto;

public class SizeDto : BaseDto<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
	public string Slug { get; set; } = string.Empty;
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Size, SizeDto>();
        }
    }
}

