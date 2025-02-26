using Catalog.Application.Features.CategoryFeature.Dto;

namespace Catalog.Application.Features.SizeFeature.Dto;

public class SizeDetailDto
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	public string Slug { get; set; } = string.Empty;
	public List<CategoryViewDto> Categories { get; set; } = new List<CategoryViewDto>();
}
