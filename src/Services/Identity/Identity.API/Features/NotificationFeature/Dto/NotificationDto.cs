namespace Identity.API.Features.NotificationFeature.Dto;

public class NotificationDto
{
	public Guid? UserId { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Content { get; set; } = string.Empty;
	public string? Navigate { get; set; } = string.Empty;
	public DateTime? CreatedDate { get; set; }
	private class Mapping : Profile
	{
		public Mapping()
		{
			CreateMap<Notification, NotificationDto>();
		}
	}
}
