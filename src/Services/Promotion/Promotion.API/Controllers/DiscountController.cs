using BuildingBlock.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using Promotion.API.Features.DiscountFeature.Queries;
using Promotion.API.Features.DiscountFeature.Commands;
using Promotion.API.Models;
using Microsoft.AspNetCore.Authorization;
namespace Promotion.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    [Authorize]
    public class DiscountController : BaseController
	{
        [HttpGet]
		public async Task<ActionResult> GetAll([FromQuery] BaseRequest request)
		{
			return Ok(await Mediator.Send(new Discount_GetAllQuery(request)));
        }

		[HttpGet("product/{ids}")]
		[AllowAnonymous]
		public async Task<ActionResult> GetByProduct([FromRoute] string ids)
		{
			return Ok(await Mediator.Send(new Discount_GetByProductQuery(ids)));
		}

		[HttpGet("product/option/{ids}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetByProductOption([FromRoute] string ids)
		{
			return Ok(await Mediator.Send(new Discount_GetOptionByProductQuery(ids)));
		}

		[HttpGet("filter")]
		public async Task<IActionResult> GetFilter([FromQuery] FilterRequest request)
		{
			return Ok(await Mediator.Send(new Discount_GetFilterQuery(request)));
        }

		[HttpGet("pagination")]
		public async Task<IActionResult> GetPagination([FromQuery] PaginationRequest request)
		{
			return Ok(await Mediator.Send(new Discount_GetPaginationQuery(request)));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
            return Ok(await Mediator.Send(new Discount_GetByIdQuery(id)));
        }

		[HttpGet("code/{code}")]
		public async Task<IActionResult> GetByCode([FromRoute] string code)
		{
			return Ok(await Mediator.Send(new Discount_GetByCodeQuery(code)));
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] DiscountAddOrUpdateRequest request)
		{
			request.CreatedUser = GetUserId();
			return Ok(await Mediator.Send(new Discount_CreateCommand(request)));
        }

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] DiscountAddOrUpdateRequest request)
		{
			request.ModifiedUser = GetUserId();
			return Ok(await Mediator.Send(new Discount_UpdateCommand(request)));
		}

		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
		{
			request.ModifiedUser = GetUserId();
			return Ok(await Mediator.Send(new Discount_DeleteCommand(request)));
		}
	}
}
