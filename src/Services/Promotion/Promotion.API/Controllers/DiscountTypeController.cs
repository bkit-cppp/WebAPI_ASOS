using BuildingBlock.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using Promotion.API.Models;
using Promotion.API.Features.DiscountTypeFeature.Queries;
using Promotion.API.Features.DiscountTypeFeature.Commands;
using Microsoft.AspNetCore.Authorization;
namespace Promotion.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DiscountTypeController : BaseController
	{
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
		{
			return Ok(await Mediator.Send(new DiscountType_GetAllQuery(request)));
        }

		[HttpGet("filter")]
		public async Task<IActionResult> GetFilter([FromQuery] FilterRequest request)
		{
			return Ok(await Mediator.Send(new DiscountType_GetFilterQuery(request)));
        }

		[HttpGet("pagination")]
		public async Task<IActionResult> GetPagination([FromQuery] PaginationRequest request)
		{
			return Ok(await Mediator.Send(new DiscountType_GetPaginationQuery(request)));
        }

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] string id)
		{
			return Ok(await Mediator.Send(new DiscountType_GetByIdQuery(id)));
        }

		[HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] DiscountTypeAddOrUpdateRequest request)
		{
			request.CreatedUser = GetUserId();
			return Ok(await Mediator.Send(new DiscountType_CreateCommand(request)));
        }

		[HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] DiscountTypeAddOrUpdateRequest request)
		{
			request.ModifiedUser = GetUserId();
			return Ok(await Mediator.Send(new DiscountType_UpdateCommand(request)));
        }

		[HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
		{
			request.ModifiedUser = GetUserId();
			return Ok(await Mediator.Send(new DiscountType_DeleteCommand(request))); ;
		}
	}
}
