using BuildingBlock.Caching.Attributes;
using Catalog.Application.Features.ColorFeature.Commands;
using Catalog.Application.Features.ColorFeature.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ColorController : BaseController
	{
		private const int _cacheTime = 30;

		[HttpGet]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
		{
			return Ok(await Mediator.Send(new Color_GetAllQuery(request)));
		}

		[HttpGet("filter")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetFilter([FromQuery] FilterRequest request)
		{
            return Ok(await Mediator.Send(new Color_GetFilterQuery(request)));
        }

		[HttpGet("pagination")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetPagination([FromQuery] PaginationRequest request)
		{
            return Ok(await Mediator.Send(new Color_GetPaginationQuery(request)));
        }

		[HttpGet("{id}")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetById(Guid id)
		{
			return Ok(await Mediator.Send(new Color_GetByIdQuery(id)));
		}

		[HttpGet("slug/{slug}")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetBySlug([FromRoute] string slug)
		{
			return Ok( await Mediator.Send(new Color_GetBySlugQuery (slug)));
		}

		[HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Color request)
		{
            return Ok(await Mediator.Send(new Color_AddCommand(request)));
        }

		[HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(Color request)
		{
            return Ok(await Mediator.Send(new Color_UpdateCommand(request)));
        }

		[HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(DeleteRequest request)
		{
            return Ok(await Mediator.Send(new Color_DeleteCommand(request)));
        }
	}
}
