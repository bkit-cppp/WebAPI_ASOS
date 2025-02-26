using Catalog.Application.Features.RatingFeature.Commands;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    [Authorize]
    public class RatingController : BaseController
	{
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
		{
			return Ok(request);
		}

		[HttpGet("filter")]
		public async Task<IActionResult> GetFilter([FromQuery] FilterRequest request)
		{
			return Ok(request);
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
		public async Task<IActionResult> Create(Rating request)
		{
			request.UserId = GetUserId();
			request.CreatedUser = GetUserId();
			request.ModifiedUser = GetUserId();
			return Ok(await Mediator.Send(new Rating_AddCommand(request)));
		}

		[HttpPut]
		public async Task<IActionResult> Update(AddOrUpdateRequest request)
		{
			return Ok(request);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(DeleteRequest request)
		{
			request.ModifiedUser = GetUserId();
			return Ok(await Mediator.Send(new Rating_DeleteCommand(request)));
		}
	}
}
