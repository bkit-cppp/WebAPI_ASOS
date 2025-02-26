using BuildingBlock.Caching.Attributes;
using Catalog.Application.Features.CategoryFeature.Commands;
using Catalog.Application.Features.CategoryFeature.Queries;
using Catalog.Application.Features.SizeFeature.Queries;
using Catalog.Application.Models.CategoryModel;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : BaseController
	{
		private const int _cacheTime = 30;

		[HttpGet]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetAll([FromQuery] CategoryGetAllRequest request)
        {
            return Ok(await Mediator.Send(new Category_GetAllQuery(request)));
        }

        [HttpGet("filter")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetFilter([FromQuery] FilterRequest request)
        {
            return Ok(await Mediator.Send(new Category_GetFilterQuery(request)));
        }

        [HttpGet("pagination")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetPagination([FromQuery] CategoryPaginationRequest request)
        {
            return Ok(await Mediator.Send(new Category_GetPaginationQuery(request)));
        }

        [HttpGet("{id}")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new Category_GetByIdQuery(id)));
        }

		[HttpGet("gender/{id}")]
		public async Task<IActionResult> GetByGender(Guid id)
		{
			return Ok(await Mediator.Send(new Category_GetByGenderQuery(id)));
		}

		[HttpGet("gender/slug/{slug}")]
		public async Task<IActionResult> GetByGenderSlug(string slug)
		{
			return Ok(await Mediator.Send(new Category_GetByGenderSlugQuery(slug)));
		}

		[HttpGet("slug/{slug}")]
		[Cache(_cacheTime)]
		public async Task<IActionResult> GetBySlug([FromRoute] string slug)
        {
            return Ok(await Mediator.Send(new Category_GetBySlugQuery(slug)));
        }

        [HttpGet("size/{size}")]
        public async Task<IActionResult> GetBySizeCategoryId(Guid size)
        {
            return Ok(await Mediator.Send(new Category_GetBySizeQuery(size)));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CategoryAddOrUpdateRequest request)
        {
            return Ok(await Mediator.Send(new Category_AddCommand(request)));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(CategoryAddOrUpdateRequest request)
        {
            return Ok(await Mediator.Send(new Category_UpdateCommand(request)));
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(DeleteRequest request)
        {
            return Ok(await Mediator.Send(new Category_DeleteCommand(request)));
        }
    }
}
