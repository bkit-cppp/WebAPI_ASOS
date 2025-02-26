namespace Identity.API.Interfaces;

public interface INotificationClient
{
	Task ReceiveNotification(string message);
}
