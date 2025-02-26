namespace BuildingBlock.Messaging.Events;

public class UserUpdatedEvent
{
	public Guid Id { get; init; }
	public string Email { get; init; }
	public string Fullname { get; init; }
	public string Avatar { get; init; }
}
