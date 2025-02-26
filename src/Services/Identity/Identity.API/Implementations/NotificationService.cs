using Identity.API.Features.NotificationFeature.Dto;
using Identity.API.Hubs;
using Identity.API.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Identity.API.Implementations;

public class NotificationService : INotificationService
{
	private readonly IHubContext<NotificationHub, INotificationClient> _hub;
	private readonly DataContext _context;
	private readonly IMapper _mapper;
	public NotificationService(
		IHubContext<NotificationHub, INotificationClient> hub, 
		DataContext context, IMapper mapper)
	{
		_hub = hub;
		_context = context;
		_mapper = mapper;
	}

	public async Task OnConnectAsync(Guid userId, string connectionId)
	{
		var connection = new HubConnection()
		{
			Id = Guid.NewGuid(),
			UserId = userId,
			ConnectionId = connectionId
		};
		_context.HubConnections.Add(connection);
		await _context.SaveChangesAsync();
	}

	public async Task OnDisconnectAsync(string connectionId)
	{
		var connection = await _context.HubConnections
							   .FirstOrDefaultAsync(s => s.ConnectionId == connectionId);
		if (connection != null)
		{
			_context.HubConnections.Remove(connection);
			await _context.SaveChangesAsync();
		}
	}

	public async Task SendNotification(Notification notification)
	{
		var connections = await _context.HubConnections
										.Where(s => s.UserId == notification.UserId)
										.Select(s => s.ConnectionId)
										.ToListAsync();

		if (connections.Any())
		{
			var dto = _mapper.Map<NotificationDto>(notification);
			var jsonNotification = JsonConvert.SerializeObject(dto);
			foreach (var connection in connections)
			{
				await _hub.Clients.Client(connection).ReceiveNotification(jsonNotification);
			}
		}
	}

	public async Task TestNotification(Notification notification)
	{
		var dto = _mapper.Map<NotificationDto>(notification);
		var jsonNotification = JsonConvert.SerializeObject(dto);
		await _hub.Clients.All.ReceiveNotification(jsonNotification);
	}
}
