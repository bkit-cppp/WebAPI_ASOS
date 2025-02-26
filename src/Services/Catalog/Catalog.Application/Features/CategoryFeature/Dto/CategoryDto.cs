using Catalog.Application.Commons.Models;

namespace Catalog.Application.Features.CategoryFeature.Dto;

public class CategoryDto : BaseDto<Guid>
{
	public string Slug { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	public string? ImageFile { get; set; } = string.Empty;
	public Guid? ParentId { get; set; }
	private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}

