using BuildingBlock.Caching.Attributes;
using Catalog.Application.Features.SizeFeature.Commands;
using Catalog.Application.Features.SizeFeature.Queries;
using Catalog.Application.Models.SizeModel;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SizeController : BaseController
	{
		private const int _cacheTime = 30;

		[HttpGet]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
        {
            return Ok(await Mediator.Send(new Size_GetAllQuery(request)));
        }

		[HttpGet("product/{id}")]
		public async Task<IActionResult> GetAllByProduct([FromQuery] BaseRequest request, [FromRoute] Guid id)
		{
			return Ok(await Mediator.Send(new Size_GetByProductQuery(request,id)));
		}

		[HttpGet("product-item/{id}")]
		public async Task<IActionResult> GetAllByProductItem([FromQuery] BaseRequest request, [FromRoute] Guid id)
		{
			return Ok(await Mediator.Send(new Size_GetByProductItemQuery(request, id)));
		}

		[HttpGet("filter")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetFilter([FromQuery] FilterRequest request)
        {
            return Ok(await Mediator.Send(new Size_GetFilterQuery(request)));
        }

        [HttpGet("pagination")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetPagination([FromQuery] PaginationRequest request)
        {
            return Ok(await Mediator.Send(new Size_GetPaginationQuery(request)));
        }

        [HttpGet("{id}")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new Size_GetByIdQuery(id)));
        }

        [HttpGet("slug/{slug}")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetBySlug([FromRoute] string slug)
        {
            return Ok(await Mediator.Send(new Size_GetBySlugQuery(slug)));
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetBySizeSizeId(Guid id)
        {
            return Ok(await Mediator.Send(new Size_GetDetailQuery(id)));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(SizeAddOrUpdate request)
        {
            return Ok(await Mediator.Send(new Size_AddCommand(request)));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(SizeAddOrUpdate request)
        {
            return Ok(await Mediator.Send(new Size_UpdateCommand(request)));
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(DeleteRequest request)
        {
            return Ok(await Mediator.Send(new Size_DeleteCommand(request)));
        }
    }
}
