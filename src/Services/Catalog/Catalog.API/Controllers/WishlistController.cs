using Catalog.Application.Features.WishListFeature.Commands;
using Catalog.Application.Features.WishListFeature.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    [Authorize]
    public class WishlistController : BaseController
	{
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
		{
			return Ok(request);
		}

		[HttpGet("filter")]
		public async Task<IActionResult> GetFilter([FromQuery] FilterRequest request)
		{
			request.UserId = GetUserId();
			return Ok(await Mediator.Send(new WishList_GetFilterQuery(request)));
		}

		[HttpGet("pagination")]
		public async Task<IActionResult> GetPagination([FromQuery] PaginationRequest request)
		{
			return Ok(request);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			return Ok(id);
		}

		[HttpPost]
		public async Task<IActionResult> Create(WishList request)
		{
			request.UserId = GetUserId();
			return Ok(await Mediator.Send(new WishList_AddCommand(request)));
		}

		[HttpPut]
		public async Task<IActionResult> Update(WishList request)
		{
			request.UserId = GetUserId();
			return Ok(await Mediator.Send(new WishList_UpdateCommand(request)));
		}

		[HttpDelete("product/{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var user = GetUserId();
			return Ok(await Mediator.Send(new WishList_DeleteByProduct(id,user)));
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(DeleteRequest request)
		{
			request.ModifiedUser = GetUserId();
			return Ok(await Mediator.Send(new WishList_DeleteCommand(request)));
		}
	}
}
