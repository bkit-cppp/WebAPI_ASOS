using BuildingBlock.Messaging.Events;
using Identity.API.Interfaces;
using MassTransit;

namespace Identity.API.Features.NotificationFeature.Consumers;

public class NotificationOrderUpdated : IConsumer<OrderStatusUpdatedEvent>
{
	private readonly DataContext _context;
	private readonly INotificationService _notificationService;
	public NotificationOrderUpdated(DataContext context, INotificationService notificationService)
	{
		_context = context;
		_notificationService = notificationService;
	}

	public async Task Consume(ConsumeContext<OrderStatusUpdatedEvent> consumer)
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
			Content = $"Your order was {consumer.Message.Status}",
			Title = $"Order {consumer.Message.Status}",
			Navigate = "",
			CreatedDate = DateTime.Now,
			DeleteFlag = false
		};

		await _notificationService.SendNotification(notification);
		_context.Notifications.Add(notification);

		await _context.SaveChangesAsync();
	}
}
