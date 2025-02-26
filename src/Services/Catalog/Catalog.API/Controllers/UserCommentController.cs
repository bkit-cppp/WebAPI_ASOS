using Microsoft.AspNetCore.Authorization;

namespace Catalog.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    [Authorize]
    public class UserCommentController : BaseController
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
		public async Task<IActionResult> Create(AddOrUpdateRequest request)
		{
			return Ok(request);
		}

		[HttpPut]
		public async Task<IActionResult> Update(AddOrUpdateRequest request)
		{
			return Ok(request);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(DeleteRequest request)
		{
			return Ok(request);
		}
	}
}
