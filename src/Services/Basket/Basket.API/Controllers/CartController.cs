using Basket.API.Enums;
using Basket.API.Features.CartFeature.Commands;
using Basket.API.Features.CartFeature.Queries;
using BuildingBlock.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	[ApiController]
	public class CartController : BaseController
	{
		[HttpGet]
		public async Task<IActionResult> GetUserBasket()
		{
			var user = GetUserId();
			return Ok(await Mediator.Send(new Cart_GetByUserQuery(user)));
		}

		[HttpDelete]
		public async Task<IActionResult> ClearCart()
		{
			return Ok(await Mediator.Send(new Cart_ClearCommand(GetUserId())));
		}

		[HttpPost("add-product/{variationId}")]
		public async Task<IActionResult> AddItem(Guid variationId)
		{
			CartAddOrDeleteProductRequest request = new CartAddOrDeleteProductRequest
			{
				UserId = GetUserId(),
				VariationId = variationId
			};
			return Ok(await Mediator.Send(new Cart_AddProductCommand(request)));
		}

		[HttpDelete("remove-product/{variationId}")]
		public async Task<IActionResult> DeleteItem(Guid variationId)
		{
			CartAddOrDeleteProductRequest request = new CartAddOrDeleteProductRequest
			{
				UserId = GetUserId(),
				VariationId = variationId
			};
			return Ok(await Mediator.Send(new Cart_DeleteProductCommand(request)));
		}

		[HttpPost("increase-quantity/{variationId}")]
		public async Task<IActionResult> IncreaseItem(Guid variationId)
		{
			CartUpdateQuantityRequest request = new CartUpdateQuantityRequest
			{
				UserId = GetUserId(),
				VariationId = variationId,
				Update = CartUpdate.Increase
			};
			return Ok(await Mediator.Send(new Cart_UpdateQuantityCommand(request)));
		}

		[HttpDelete("decrease-quantity/{variationId}")]
		public async Task<IActionResult> DecreaseItem(Guid variationId)
		{
			CartUpdateQuantityRequest request = new CartUpdateQuantityRequest
			{
				UserId = GetUserId(),
				VariationId = variationId,
				Update = CartUpdate.Decrease
			};
			return Ok(await Mediator.Send(new Cart_UpdateQuantityCommand(request)));
		}

		[HttpPost("apply-discount/{code}")]
		public async Task<IActionResult> ApplyDisocunt(string code)
		{
			DiscountAddOrDeleteRequest request = new DiscountAddOrDeleteRequest
			{
				UserId = GetUserId(),
				Code = code
			};
			return Ok(await Mediator.Send(new Cart_ApplyDiscountCommand(request)));
		}

		[HttpDelete("remove-discount/{code}")]
		public async Task<IActionResult> RemoveDiscount(string code)
		{
			DiscountAddOrDeleteRequest request = new DiscountAddOrDeleteRequest
			{
				UserId = GetUserId(),
				Code = code
			};
			return Ok(await Mediator.Send(new Cart_DeleteDiscountCommand(request)));
		}

        [HttpPost("apply-point/{point}")]
        public async Task<IActionResult> ApplyPoint(int point)
        {
			var user = GetUserId();
            return Ok(await Mediator.Send(new Cart_ApplyPointCommand(user,point)));
        }

        [HttpDelete("remove-point")]
        public async Task<IActionResult> RemovePoint()
        {
            var user = GetUserId();
            return Ok(await Mediator.Send(new Cart_DeletePointCommand(user)));
        }
    }
}
