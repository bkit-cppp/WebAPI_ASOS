using BuildingBlock.Caching.Attributes;
using Catalog.Application.Features.BrandFeature.Commands;
using Catalog.Application.Features.BrandFeature.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BrandController : BaseController
	{
		private const int _cacheTime = 30;

		[HttpGet]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
		{
			return Ok(await Mediator.Send(new Brand_GetAllQuery(request)));
		}

		[HttpGet("filter")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetFilter([FromQuery] FilterRequest request)
		{
            return Ok(await Mediator.Send(new Brand_GetFilterQuery(request)));
        }

		[HttpGet("pagination")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetPagination([FromQuery] PaginationRequest request)
		{
            return Ok(await Mediator.Send(new Brand_GetPaginationQuery(request)));
        }

		[HttpGet("{id}")]
		[Cache(10)]
		public async Task<IActionResult> GetById(Guid id)
		{
			return Ok(await Mediator.Send(new Brand_GetByIdQuery(id)));
		}

		[HttpGet("slug/{slug}")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetBySlug([FromRoute] string slug)
		{
			return Ok( await Mediator.Send(new Brand_GetBySlugQuery (slug)));
		}

		[HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Brand request)
		{
            return Ok(await Mediator.Send(new Brand_AddCommand(request)));
        }

		[HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(Brand request)
		{
            return Ok(await Mediator.Send(new Brand_UpdateCommand(request)));
        }

		[HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(DeleteRequest request)
		{
            return Ok(await Mediator.Send(new Brand_DeleteCommand(request)));
        }
	}
}
