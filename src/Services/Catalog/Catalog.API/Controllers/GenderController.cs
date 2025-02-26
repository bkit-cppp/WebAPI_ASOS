using BuildingBlock.Caching.Attributes;
using Catalog.Application.Features.GenderFeature.Commands;
using Catalog.Application.Features.GenderFeature.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GenderController : BaseController
	{
		private const int _cacheTime = 30;

		[HttpGet]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
		{
			return Ok(await Mediator.Send(new Gender_GetAllQuery(request)));
		}

		[HttpGet("menu")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetMenu()
		{
			return Ok(await Mediator.Send(new Gender_GetWithCategoryQuery()));
		}

		[HttpGet("filter")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetFilter([FromQuery] FilterRequest request)
		{
			return Ok(await Mediator.Send(new Gender_GetFilterQuery(request)));
		}

		[HttpGet("pagination")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetPagination([FromQuery] PaginationRequest request)
		{
			return Ok(await Mediator.Send(new Gender_GetPaginationQuery(request)));
		}

		[HttpGet("{id}")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			return Ok(await Mediator.Send(new Gender_GetByIdQuery(id)));
		}

		[HttpGet("slug/{slug}")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetBySlug([FromRoute] string slug)
		{
			return Ok(await Mediator.Send(new Gender_GetBySlugQuery(slug)));
		}

		[HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Gender request)
		{
			return Ok(await Mediator.Send(new Gender_AddCommand(request)));
		}

		[HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(Gender request)
		{
			return Ok(await Mediator.Send(new Gender_UpdateCommand(request)));
		}

		[HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(DeleteRequest request)
		{
			return Ok(await Mediator.Send(new Gender_DeleteCommand(request)));
		}
	}
}
