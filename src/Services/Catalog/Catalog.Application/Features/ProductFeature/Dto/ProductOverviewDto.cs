namespace Catalog.Application.Features.ProductFeature.Dto;

public class ProductOverviewDto
{
	public Guid Id { get; set; }
	public string Slug { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string Image { get; set; } = string.Empty;
	public decimal AverageRating { get; set; } = 0;
	public int? Bought { get; set; } = 0;
	public decimal OriginalPrice { get; set; } = 0;
	public decimal SalePrice { get; set; } = 0;
	public bool IsSale { get; set; } = false;
	public string? Category { get; set; }
	public string? Brand { get; set; }
	private class Mapping : Profile
	{
		public Mapping()
		{
			CreateMap<Product, ProductOverviewDto>()
				.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.CategoryGender != null ? src.CategoryGender.Category.Name : ""))
				.ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : ""));
		}
	}
}
