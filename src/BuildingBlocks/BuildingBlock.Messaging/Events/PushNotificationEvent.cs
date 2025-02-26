namespace BuildingBlock.Messaging.Events;

public class PushNotificationEvent
{
	public Guid? UserId { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Content { get; set; } = string.Empty;
	public string? Navigate { get; set; } = string.Empty;
}
