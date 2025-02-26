using BuildingBlock.Core.WebApi;
using Identity.API.Features.NotificationFeature.Commands;
using Identity.API.Features.NotificationFeature.Queries;
using Identity.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    [Authorize]
    public class NotificationController : BaseController
	{
		private readonly INotificationService _service;
		public NotificationController(INotificationService service)
		{
			_service = service;
		}
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
		{
			return Ok(await Mediator.Send(new Notification_GetAllQuery(request)));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			return Ok(await Mediator.Send(new Notification_GetByIdQuery(id)));
		}

		[HttpGet("filter")]
		public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
		{
			request.UserId = GetUserId();
			return Ok(await Mediator.Send(new Notification_GetFilterQuery(request)));
		}

		[HttpGet("pagination")]
		public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
		{
			request.UserId = GetUserId();
			return Ok(await Mediator.Send(new Notification_GetPaginationQuery(request)));
		}

		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
		{
			request.ModifiedUser = GetUserId();
			return Ok(await Mediator.Send(new Notification_DeleteCommand(request)));
		}

		[HttpGet("test-notification")]
		public async Task<IActionResult> Add()
		{
			var notification = new Notification()
			{
				UserId = Guid.NewGuid(),
				Content = "Hello world !!!",
				Title = "Test notification",
				Navigate = "/account/my-order"
			};
			await _service.TestNotification(notification);
			return Ok(notification);
		}
	}
}
