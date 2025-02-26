using BuildingBlock.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using Promotion.API.Models;
using Promotion.API.Features.DiscountProductFeature.Queries;
using Promotion.API.Features.DiscountProductFeature.Commands;
using Microsoft.AspNetCore.Authorization;
namespace Promotion.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    [Authorize]
    public class DiscountProductController : BaseController
	{
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
		{
			return Ok(await Mediator.Send(new DiscountProduct_GetAllQuery(request)));
        }

		[HttpGet("pagination")]
		public async Task<IActionResult> GetPagination([FromQuery] PaginationRequest request)
		{
			return Ok(await Mediator.Send(new DiscountProduct_GetPaginationQuery(request)));
        }

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			return Ok(await Mediator.Send(new DiscountProduct_GetByIdQuery(id)));
        }

		[HttpPost]
		public async Task<IActionResult> Create([FromBody]DiscountProductAddOrUpdateRequest request)
		{
			request.CreatedUser = GetUserId();
			return Ok(await Mediator.Send(new DiscountProduct_CreateCommand(request)));
        }

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] DiscountProductAddOrUpdateRequest request)
		{
			request.ModifiedUser = GetUserId();
			return Ok(await Mediator.Send(new DiscountProduct_UpdateCommand(request)));
        }

		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
		{
			request.ModifiedUser = GetUserId();
			return Ok(await Mediator.Send(new DiscountProduct_DeleteCommand(request)));
        }
	}
}
