namespace BuildingBlock.Messaging.Events;

public class PushEmailEvent
{
	public Guid UserId { get; set; }
	public string Email { get; set; } = string.Empty;
	public string Type { get; set; }
}
