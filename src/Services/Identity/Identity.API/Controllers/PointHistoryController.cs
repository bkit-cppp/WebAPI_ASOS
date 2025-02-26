using BuildingBlock.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using Identity.API.Features.PointHistoryFeature.Queries;
using Identity.API.Features.PointHistoryFeature.Commands;
using Microsoft.AspNetCore.Authorization;
using BuildingBlock.Caching.Attributes;
using BuildingBlock.Core.Filters;

namespace Identity.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    [Authorize]
    public class PointHistoryController : BaseController
	{
		[HttpGet]
		//[Cache(10000)]
		public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
		{
			return Ok(await Mediator.Send(new PointHistory_GetAllQuery(request)));
		}

		[HttpGet("{id}")]
		//[Cache(10000)]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			return Ok(await Mediator.Send(new PointHistory_GetByIdQuery(id)));
		}

        [HttpGet("filter")]
		//[TokenExtraction]
		//[Cache(10000,true)]
		public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
        {
			request.UserId = GetUserId();
			return Ok(await Mediator.Send(new PointHistory_GetFilterQuery(request)));
        }

        [HttpGet("pagination")]
		//[TokenExtraction]
		//[Cache(10000, true)]
		public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
        {
            request.UserId = GetUserId();
            return Ok(await Mediator.Send(new PointHistory_GetPaginationQuery(request)));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
        {
            return Ok(await Mediator.Send(new PointHistory_DeleteCommand(request)));
        }

    }
}
