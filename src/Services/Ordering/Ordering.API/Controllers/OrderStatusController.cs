using Ordering.API.Features.OrderStatusFeature.Commands;
using Ordering.API.Features.OrderStatusFeature.Queries;


namespace Ordering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : BaseController
    {
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await Mediator.Send(new OrderStatus_GetByIdQuery(id)));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
        {
            return Ok(await Mediator.Send(new OrderStatus_GetAllQuery(request)));
        }
       
        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
        {
            return Ok(await Mediator.Send(new OrderStatus_GetFilterQuery(request)));
        }

        [HttpPut("updateStatus")]
        [Authorize]
        public async Task<IActionResult> UpdateStatus([FromBody] OrderStatusAddOrUpdate request)
        {
            request.ModifiedUser = GetUserId();
            return Ok(await Mediator.Send(new OrderStatus_UpdateCommand(request)));
        }
    }
}
