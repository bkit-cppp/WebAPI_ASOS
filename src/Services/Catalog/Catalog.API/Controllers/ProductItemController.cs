using Catalog.Application.Features.ProductItemFeature.Commands;
using Catalog.Application.Features.ProductItemFeature.Queries;
using Catalog.Application.Models.ProductItemModel;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductItemController : BaseController
	{
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
		{
            return Ok(await Mediator.Send(new ProductItem_GetAllQuery(request)));
        }

		[HttpGet("filter")]
		public async Task<IActionResult> GetFilter([FromQuery] FilterRequest request)
		{
            return Ok(await Mediator.Send(new ProductItem_GetFilterQuery(request)));
        }

		[HttpGet("pagination")]
		public async Task<IActionResult> GetPagination([FromQuery] ProductItemPaginationRequest request)
		{
            return Ok(await Mediator.Send(new ProductItem_GetPaginationQuery(request)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new ProductItem_GetByIdQuery(id)));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ProductItem request)
        {
            return Ok(await Mediator.Send(new ProductItem_AddCommand(request)));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(ProductItem request)
        {
            return Ok(await Mediator.Send(new ProductItem_UpdateCommand(request)));
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(DeleteRequest request)
        {
            return Ok(await Mediator.Send(new ProductItem_DeleteCommand(request)));
        }

		[HttpDelete("images")]
        [Authorize]
        public async Task<IActionResult> DeleteImage(DeleteRequest request)
		{
			return Ok(await Mediator.Send(new ProductItem_DeleteImageCommand(request)));
		}

		[HttpPost("images")]
        [Authorize]
        public async Task<IActionResult> AddImage(AddProductImage request)
		{
			return Ok(await Mediator.Send(new ProductItem_AddImageCommand(request)));
		}
	}
}
