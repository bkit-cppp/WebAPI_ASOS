using Catalog.Application.Features.SizeCategoryFeature.Commands;
using Catalog.Application.Features.SizeCategoryFeature.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SizeCategoryController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
        {
            return Ok(await Mediator.Send(new SizeCategory_GetAllQuery(request)));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilter([FromQuery] FilterRequest request)
        {
            return Ok(await Mediator.Send(new SizeCategory_GetFilterQuery(request)));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetPagination([FromQuery] PaginationRequest request)
        {
            return Ok(await Mediator.Send(new SizeCategory_GetPaginationQuery(request)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new SizeCategory_GetByIdQuery(id)));
        }

        [HttpGet("CategoryId/{CategoryId}")]
        public async Task<IActionResult> GetByCategoryId(Guid CategoryId)
        {
            return Ok(await Mediator.Send(new SizeCategory_GetByCategoryIdQuery(CategoryId)));
        }

        [HttpGet("SizeId/{SizeId}")]
        public async Task<IActionResult> GetBySizeId(Guid SizeId)
        {
            return Ok(await Mediator.Send(new SizeCategory_GetBySizeIdQuery(SizeId)));
        }

        [HttpPost]
        public async Task<IActionResult> Create(SizeCategory request)
        {
            return Ok(await Mediator.Send(new SizeCategory_AddCommand(request)));
        }

        [HttpPut]
        public async Task<IActionResult> Update(SizeCategory request)
        {
            return Ok(await Mediator.Send(new SizeCategory_UpdateCommand(request)));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteRequest request)
        {
            return Ok(await Mediator.Send(new SizeCategory_DeleteCommand(request)));
        }
    }
}
