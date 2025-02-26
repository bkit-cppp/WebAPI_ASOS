using Catalog.Application.Features.VariationFeature.Commands;
using Catalog.Application.Features.VariationFeature.Queries;
using Catalog.Application.Models.VariationModel;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariationController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
        {
            return Ok(await Mediator.Send(new Variation_GetAllQuery(request)));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilter([FromQuery] FilterRequest request)
        {
            return Ok(await Mediator.Send(new Variation_GetFilterQuery(request)));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetPagination([FromQuery] VariationPaginationRequest request)
        {
            return Ok(await Mediator.Send(new Variation_GetPaginationQuery(request)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new Variation_GetByIdQuery(id)));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Variation request)
        {
            return Ok(await Mediator.Send(new Variation_AddCommand(request)));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(Variation request)
        {
            return Ok(await Mediator.Send(new Variation_UpdateCommand(request)));
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(DeleteRequest request)
        {
            return Ok(await Mediator.Send(new Variation_DeleteCommand(request)));
        }
    }
}
