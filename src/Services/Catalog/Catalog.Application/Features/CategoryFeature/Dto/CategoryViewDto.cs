namespace Catalog.Application.Features.CategoryFeature.Dto;

public class CategoryViewDto
{
	public Guid Id { get; set; }
	public string Slug { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public bool Selected { get; set; } = false;
	private class Mapping : Profile
	{
		public Mapping()
		{
			CreateMap<Category, CategoryViewDto>()
				.ForMember(dest => dest.Selected, opt => opt.MapFrom(src => src.SizeCategories == null ? false : src.SizeCategories.Count() > 0));
		}
	}
}
