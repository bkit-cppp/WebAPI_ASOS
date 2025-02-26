namespace BuildingBlock.Messaging.Events;

public class PointUpdatedEvent
{
	public int PointChange { get; set; }
	public Guid UserId { get; set; }
	public string ReferenceId { get; set; } = string.Empty;
	public string ReferenceType { get; set; } = string.Empty;
	public string Reason { get; set; } = string.Empty;
}
