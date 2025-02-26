using BuildingBlock.Caching.Attributes;
using Catalog.Application.Features.CommentFeature.Commands;
using Catalog.Application.Features.CommentFeature.Queries;
using Catalog.Application.Models.CommentModel;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentController : BaseController
	{
		private const int _cacheTime = 30;

		[HttpGet("product/{id}")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetAllProduct(Guid id, [FromQuery] BaseRequest request)
		{
			return Ok(await Mediator.Send(new Comment_GetAllQuery(id,request)));
		}

		[HttpGet("filter")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetFilter([FromQuery] CommentFilterRequest request)
		{
			return Ok(await Mediator.Send(new Comment_GetFilterQuery(request)));
		}

		[HttpGet("pagination")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetPagination([FromQuery] PaginationRequest request)
		{
			return Ok(await Mediator.Send(new Comment_GetPaginationQuery(request)));
		}

		[HttpGet("{id}")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			return Ok(await Mediator.Send(new Comment_GetByIdQuery(id)));
		}

		[HttpGet("parent/{id}")]
		public async Task<IActionResult> GetByParentId([FromRoute] Guid id)
		{
			return Ok(id);
		}

		[HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Comment request)
		{
			request.UserId = GetUserId();
			return Ok(await Mediator.Send(new Comment_AddCommand(request)));
		}

		[HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(Comment request)
		{
			request.UserId = GetUserId();
			return Ok(await Mediator.Send(new Comment_UpdateCommand(request)));
		}

		[HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(DeleteRequest request)
		{
			request.ModifiedUser = GetUserId();
			return Ok(await Mediator.Send(new Comment_DeleteCommand(request)));
		}
	}
}
