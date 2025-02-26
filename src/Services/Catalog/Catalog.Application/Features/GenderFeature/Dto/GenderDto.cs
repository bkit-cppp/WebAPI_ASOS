namespace Catalog.Application.Features.GenderFeature.Dto;

public class GenderDto
{
	public Guid Id { get; set; }
	public string Slug { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	private class Mapping : Profile
	{
		public Mapping()
		{
			CreateMap<Gender, GenderDto>();
		}
	}
}
