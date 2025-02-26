using Ordering.API.Features.OrderHistoryFeature.Command;
using Ordering.API.Features.OrderHistoryFeature.Queries;

namespace Ordering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderHistoryController : BaseController
    {
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new OrderHistoryGetByIdQueries(id)));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
        {
            return Ok(await Mediator.Send(new OrderHistoryGetAllQueries(request)));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] OrderHistoryFilterRequest request)
        {
            return Ok(await Mediator.Send(new OrderHistoryGetFilterQueries(request)));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> Getpagination([FromQuery] PaginationRequest request)
        {
            return Ok(await Mediator.Send(new OrderHistoryGetpaginationQueries(request)));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderHistoryAddOrUpdate request)
        {
            return Ok(await Mediator.Send(new OrderHistoryCreateCommand(request)));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(DeleteRequest Id)
        {
            return Ok(await Mediator.Send(new OrderHistoryDeleteCommand(Id)));
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromBody] OrderHistoryAddOrUpdate request)
        {
            return Ok(await Mediator.Send(new OrderHistoryUpdateCommand(request)));
        }
    }
}
