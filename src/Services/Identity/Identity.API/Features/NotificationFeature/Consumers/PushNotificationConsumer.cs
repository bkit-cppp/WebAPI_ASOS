using BuildingBlock.Messaging.Events;
using Identity.API.Interfaces;
using MassTransit;

namespace Identity.API.Features.NotificationFeature.Consumers;

public class PushNotificationConsumer : IConsumer<PushNotificationEvent>
{
	private readonly DataContext _context;
	private readonly INotificationService _notificationService;
	public PushNotificationConsumer(DataContext context, INotificationService notificationService)
	{
		_context = context;
		_notificationService = notificationService;
	}

	public async Task Consume(ConsumeContext<PushNotificationEvent> consumer)
	{
		var user = await _context.Users.FindAsync(consumer.Message.UserId);
		if(user != null)
		{
			var notification = new Notification()
			{
				Id = Guid.NewGuid(),
				UserId = consumer.Message.UserId,
				Content = consumer.Message.Content,
				Title = consumer.Message.Title,
				Navigate = consumer.Message.Navigate,
				CreatedDate = DateTime.Now,
				DeleteFlag = false
			};
			await _notificationService.SendNotification(notification);
			_context.Notifications.Add(notification);
			await _context.SaveChangesAsync();
		}
	}
}
