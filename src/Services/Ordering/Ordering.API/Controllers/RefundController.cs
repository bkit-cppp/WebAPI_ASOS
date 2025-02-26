using Ordering.API.Features.RefundFeature.Command;
using Ordering.API.Features.RefundFeature.Queries;


namespace Ordering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RefundController :BaseController
    {
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new Refund_GetByIdQueries(id)));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
        {
            return Ok(await Mediator.Send(new Refund_GetAllQueries(request)));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] RefundFilterRequest request)
        {
            return Ok(await Mediator.Send(new Refund_GetFilterQueries(request)));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetPagination([FromQuery] PaginationRequest request)
        {
            return Ok(await Mediator.Send(new Refund_GetPaginationGetQueries(request)));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RefundAddOrUpdateRequest request)
        {
            return Ok(await Mediator.Send(new Refund_CreateCommand(request)));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(DeleteRequest Id)
        {
            return Ok(await Mediator.Send(new Refund_DeleteCommand(Id)));
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromBody] RefundAddOrUpdateRequest request)
        {
            return Ok(await Mediator.Send(new Refund_UpdateCommand(request)));
        }
    }
}
