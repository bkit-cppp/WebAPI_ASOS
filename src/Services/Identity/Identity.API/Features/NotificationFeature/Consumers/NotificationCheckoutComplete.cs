using BuildingBlock.Messaging.Events;
using Identity.API.Interfaces;
using MassTransit;

namespace Identity.API.Features.NotificationFeature.Consumers;

public sealed class NotificationCheckoutComplete : IConsumer<OrderCheckoutEvent>
{
	private readonly DataContext _context;
	private readonly INotificationService _notificationService;
	public NotificationCheckoutComplete(DataContext context, INotificationService notificationService)
	{
		_context = context;
		_notificationService = notificationService;
	}

	public async Task Consume(ConsumeContext<OrderCheckoutEvent> consumer)
	{
		var user = await _context.Users.FindAsync(consumer.Message.UserId);

		if (user == null)
		{
			return;
		}

		var notification = new Notification()
		{
			Id = Guid.NewGuid(),
			UserId = consumer.Message.UserId,
			Content = $"You just placed an order at {DateTime.Now.ToString("dd-mm hh:mm")}",
			Title = "Order placed",
			Navigate = "",
			CreatedDate = DateTime.Now,
			DeleteFlag = false
		};
		await _notificationService.SendNotification(notification);
		_context.Notifications.Add(notification);
		await _context.SaveChangesAsync();

	}
}