using Catalog.Application.Features.BrandFeature.Dto;
using Catalog.Application.Features.CategoryFeature.Dto;
using Catalog.Application.Features.GenderFeature.Dto;

namespace Catalog.Application.Features.ProductFeature.Dto;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal AverageRating { get; set; } = 0;
    public string? SizeAndFit { get; set; } = string.Empty;
	public string? Image { get; set; } = string.Empty;
	public int? Bought { get; set; } = 0;
	public decimal OriginalPrice { get; set; } = 0;
	public decimal SalePrice { get; set; } = 0;
	public bool IsSale { get; set; } = false;
	public CategoryDto? Category { get; set; }
	public BrandDto? Brand { get; set; }
	public GenderDto? Gender { get; set; }
	public Guid? CategoryId { get; set; }
	public Guid? BrandId { get; set; }
	public Guid? GenderId { get; set; }
	private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductDto>()
				.ForMember(dest => dest.Category, 
						   opt => opt.MapFrom(src => src.CategoryGender != null ? src.CategoryGender.Category : null))
				.ForMember(dest => dest.Gender,
						   opt => opt.MapFrom(src => src.CategoryGender != null ? src.CategoryGender.Gender : null))
				.ForMember(dest => dest.GenderId,
						   opt => opt.MapFrom(src => src.CategoryGender != null ? src.CategoryGender.GenderId : (Guid?)null))
				.ForMember(dest => dest.GenderId,
						   opt => opt.MapFrom(src => src.CategoryGender != null ? src.CategoryGender.CategoryId : (Guid?)null))
				.ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand));
        }
    }
}
