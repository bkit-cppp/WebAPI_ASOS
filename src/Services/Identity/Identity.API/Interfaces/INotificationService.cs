namespace Identity.API.Interfaces;

public interface INotificationService
{
	Task SendNotification(Notification notification);
	Task TestNotification(Notification notification);
	Task OnConnectAsync(Guid userId,string connectionId);
	Task OnDisconnectAsync(string connectionId);
}
