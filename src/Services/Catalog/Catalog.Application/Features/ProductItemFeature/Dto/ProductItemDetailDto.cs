using Catalog.Application.Features.VariationFeature.Dto;

namespace Catalog.Application.Features.ProductItemFeature.Dto;

public class ProductItemDetailDto
{
	public Guid Id { get; set; }
	public string Color { get; set; } = "";
	public decimal AdditionalPrice { get; set; } = 0;
	public List<string> Images { get; set; } = new List<string>();
	public List<VariationDetailDto> Variations { get; set; } = new List<VariationDetailDto>();
}
