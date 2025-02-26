using Ordering.API.Features.OrderFeature.Commands;
using Ordering.API.Features.OrderFeature.Dto;
using Ordering.API.Features.OrderFeature.Queries;

namespace Ordering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : BaseController
    {
        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetOrderHistoryByOrderId(Guid id)
        {
            return Ok(await Mediator.Send(new OrderHistoryGetByOrderIdQuery(id)));
        }

        [HttpGet("filter/user")]
        public async Task<IActionResult> FilterByUser( [FromQuery] FilterRequest request)
        {
            var userId = GetUserId();
            return Ok(await Mediator.Send(new OrderGetByUserFilterQuery(userId, request)));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult>GetById(Guid id)
        {
            return Ok(await Mediator.Send(new OrderGetByIdQuery(id)));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request)
        {
            return Ok(await Mediator.Send(new OrderGetPaginationQuery(request)));
        }

        [HttpGet("pagination/user")]
        public async Task<IActionResult> GetAllUser([FromQuery] PaginationRequest request)
        {
            request.UserId = GetUserId();
            return Ok(await Mediator.Send(new OrderGetPaginationQuery(request)));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
        {
            return Ok(await Mediator.Send(new OrderGetFilterQuery(request)));
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]OrderDto request)
        {
            return Ok(await Mediator.Send(new OrderCreateCommand(request)));
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteRequest request)
        {
            request.ModifiedUser = GetUserId();
            return Ok(await Mediator.Send(new OrderDeleteCommand(request)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] OrderUpdateInfoRequest request)
        {
            request.ModifiedUser = GetUserId();
            return Ok(await Mediator.Send(new OrderUpdateCommand(request)));
        }

		[HttpPut("status")]
		public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusRequest request)
		{
			request.ModifiedUser = GetUserId();
            request.RoleId = GetRole();
			return Ok(await Mediator.Send(new OrderUpdateStatusCommand(request)));
		}

		[HttpDelete("{id}/cancel")]
		public async Task<IActionResult> Cancel(Guid id)
		{
            UpdateStatusRequest request = new UpdateStatusRequest()
            {
                Id = id,
                Status = OrderStatusConstant.Canceled,
                ModifiedUser = GetUserId(),
                RoleId = GetRole()
            };
			return Ok(await Mediator.Send(new OrderUpdateStatusCommand(request)));
		}

        [HttpPost("checkout-v2")]
        public async Task<IActionResult> CheckoutV2([FromBody] CheckoutUrlRequest request)
        {
            return Ok(await Mediator.Send(new OrderCheckoutV2Command(GetUserId(),request)));
        }
    }
}
