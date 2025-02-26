namespace Catalog.Application.Features.WishListFeature.Dto;
public class WishListDto
{
	public Guid Id { get; set; }
	public string Slug { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string? Image { get; set; } = string.Empty;
	public decimal OriginalPrice { get; set; } = 0;
	public decimal SalePrice { get; set; } = 0;
	public bool IsSale { get; set; } = false;

	private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, WishListDto>();
        }
    }
}
