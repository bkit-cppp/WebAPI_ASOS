namespace Identity.API.Features.StatusFeature.Dto;

public class StatusDto
{
	public string Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	private class Mapping : Profile
	{
		public Mapping()
		{
			CreateMap<Status, StatusDto>();
		}
	}
}
