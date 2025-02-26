namespace Catalog.Application.Features.VariationFeature.Dto;

public class VariationDetailDto
{
	public Guid Id { get; set; }
	public int QtyDisplay { get; set; } = 0;
	public int QtyInStock { get; set; } = 0;
	public decimal Stock { get; set; } = 0;
	public string Size { get; set; } = "";
}
