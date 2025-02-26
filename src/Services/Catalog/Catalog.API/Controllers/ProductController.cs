using BuildingBlock.Caching.Attributes;
using Catalog.Application.Features.ProductFeature.Commands;
using Catalog.Application.Features.ProductFeature.Queries;
using Catalog.Application.Models.ProductModel;
using Microsoft.AspNetCore.Authorization;

namespace User.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : BaseController
	{

		[HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
		{
			return Ok(await Mediator.Send(new Product_GetAllQuery(request)));
		}

		[HttpGet("similar/{slug}")]
		public async Task<IActionResult> GetSimilar(string slug,int take = 15)
		{
			return Ok(await Mediator.Send(new Product_GetSimilarQuery(slug, take)));
		}

		[HttpGet("filter")]
        [Authorize]
        public async Task<IActionResult> GetFilter([FromQuery] FilterRequest request)
		{
			return Ok(await Mediator.Send(new Product_GetFilterQuery(request)));
		}

		[HttpGet("pagination")]
        [Authorize]
        public async Task<IActionResult> GetPagination([FromQuery] ProductPaginationRequest request)
		{
			return Ok(await Mediator.Send(new Product_GetPaginationQuery(request)));
		}

		[HttpGet("pagination-overview")]
		public async Task<IActionResult> GetPaginationOverview([FromQuery] ProductOverviewPaginationRequest request)
		{
			return Ok(await Mediator.Send(new Product_GetPaginationOverviewQuery(request)));
		}

		[HttpGet("newest")]
		public async Task<IActionResult> GetNewest([FromQuery] BaseRequest request)
		{
			return Ok(await Mediator.Send(new Product_GetNewestQuery(request)));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			return Ok(await Mediator.Send(new Product_GetByIdQuery(id)));
		}

		[HttpGet("detail/{slug}")]
		public async Task<IActionResult> GetDetail([FromRoute] string slug)
		{
			var user = GetUserId();
			return Ok(await Mediator.Send(new Product_GetDetailQuery(slug,user)));
		}

		[HttpGet("item/{id}")]
		public async Task<IActionResult> GetByItemId([FromRoute] Guid id)
		{
			return Ok(await Mediator.Send(new Product_GetByItemQuery(id)));
		}

		[HttpGet("slug/{slug}")]
		public async Task<IActionResult> GetBySlug([FromRoute] string slug)
		{
			return Ok(await Mediator.Send(new Product_GetBySlugQuery(slug)));
		}

		[HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ProductAddOrUpdate request)
		{
			return Ok(await Mediator.Send(new Product_AddCommand(request)));
		}

		[HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(ProductAddOrUpdate request)
		{
			return Ok(await Mediator.Send(new Product_UpdateCommand(request)));
		}

		[HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(DeleteRequest request)
		{
			return Ok(await Mediator.Send(new Product_DeleteCommand(request)));
		}

		[HttpPost("export-excel")]
        [Authorize]
        public async Task<IActionResult> ExportExcel([FromBody] ExportRequest exportRequest)
		{
			var query = new Product_ExportQuery(exportRequest);
			var excelFile = await Mediator.Send(query);
			Console.WriteLine(excelFile);
			return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Products.xlsx");
		}
	}
}
