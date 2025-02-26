using Ordering.API.Features.OrderItemFeature.Command;
using Ordering.API.Features.OrderItemFeature.Queries;


namespace Ordering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderItemController : BaseController
    {
 
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new OrderItemGetByIdQueries(id)));
        }

        [HttpGet("get-all/{orderId}")]
        public async Task<IActionResult> GetAll([FromQuery] BaseRequest request, [FromRoute] Guid orderId)
        {
            return Ok(await Mediator.Send(new OrderItemGetAllQueries(orderId,request)));
        }
     
        [HttpGet("filter/{orderId}")]
        public async Task<IActionResult> Filter([FromQuery] FilterRequest request, [FromRoute] Guid orderId)
        {
            return Ok(await Mediator.Send(new OrderItemGetFilterQueries(orderId, request)));
        }

		[HttpGet("pagination/{orderId}")]
		public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request,[FromRoute] Guid orderId)
		{
			return Ok(await Mediator.Send(new OrderItemGetPaginationQueries(orderId, request)));
		}

		[HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderItem request)
        {
            return Ok(await Mediator.Send(new OrderItemCreateCommand(request)));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(DeleteRequest Id)
        {
            return Ok(await Mediator.Send(new OrderItemDeleteCommand(Id)));
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromBody] OrderItemAddOrUpdate request)
        {
            return Ok(await Mediator.Send(new OrderItemUpdateCommand(request)));
        }
    }
}

