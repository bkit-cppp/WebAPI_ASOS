using BuildingBlock.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using Promotion.API.Features.DiscountHistoryFeature.Queries;
using Promotion.API.Features.DiscountHistoryFeature.Commands;
using Promotion.API.Models;
using Microsoft.AspNetCore.Authorization;
namespace Promotion.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class DiscountHistoryController : BaseController
	{
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
		{
            return Ok(await Mediator.Send(new DiscountHistory_GetAllQuery(request)));
        }

		[HttpGet("filter")]
		public async Task<IActionResult> GetFilter([FromQuery] DiscountHistoryFilterRequest request)
		{
            return Ok(await Mediator.Send(new DiscountHistory_GetFilterQuery(request)));
        }

		[HttpGet("pagination")]
		public async Task<IActionResult> GetPagination([FromQuery] DiscountHistoryPaginationRequest request)
		{
            return Ok(await Mediator.Send(new DiscountHistory_GetPaginationQuery(request)));
        }

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
            return Ok(await Mediator.Send(new DiscountHistory_GetByIdQuery(id)));
        }

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] DiscountHistoryAddOrUpdateRequest request)
		{
			request.CreatedUser = GetUserId();
			return Ok(await Mediator.Send(new DiscountHistory_CreateCommand(request)));
        }

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] DiscountHistoryAddOrUpdateRequest request)
		{
			request.ModifiedUser = GetUserId();
			return Ok(await Mediator.Send(new DiscountHistory_UpdateCommand(request)));
        }

		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
		{
			request.ModifiedUser = GetUserId();
			return Ok(await Mediator.Send(new DiscountHistory_DeleteCommand(request)));
        }
	}
}
