using BuildingBlock.Core.Constants;
using BuildingBlock.Utilities;
using Identity.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.IdentityModel.Tokens.Jwt;

namespace Identity.API.Hubs;


public class NotificationHub : Hub<INotificationClient>
{
	private readonly INotificationService _notificationService;
	public NotificationHub(INotificationService notificationService)
	{
		_notificationService = notificationService;
	}

	public override async Task OnConnectedAsync()
	{
		var userId = GetUserId();
		var connectionId = Context.ConnectionId;

		if (!StringHelper.GuidIsNull(userId))
		{
			await _notificationService.OnConnectAsync(userId, Context.ConnectionId);

			await Clients.Client(connectionId).ReceiveNotification(
			$"Thank you for connecting {Context.User?.Identity?.Name} ");

			await base.OnConnectedAsync();
		}
	}

	public override async Task OnDisconnectedAsync(Exception exception)
	{
		var connectionId = Context.ConnectionId;
		await _notificationService.OnDisconnectAsync(connectionId);
		await base.OnDisconnectedAsync(exception);
	}

	private Guid GetUserId()
	{
		try
		{
			var httpContext = Context.GetHttpContext();
			var handler = new JwtSecurityTokenHandler();
			var accessToken = httpContext!.Request.Query["access_token"].ToString();
			var jsonToken = handler.ReadToken(accessToken);
			var tokenS = handler.ReadToken(accessToken) as JwtSecurityToken;
			if (tokenS == null) { return Guid.Empty; }
			var id = tokenS.Claims.First(claim => claim.Type == JWTClaimsTypeConstant.Id).Value;
			return Guid.Parse(id);
		}
		catch
		{
			return Guid.Empty;
		}
	}
}
