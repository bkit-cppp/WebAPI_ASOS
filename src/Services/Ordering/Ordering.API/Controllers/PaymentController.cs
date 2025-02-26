using Ordering.API.Features.OrderFeature.Commands;
using Ordering.API.Interfaces;

namespace Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    //[Authorize]
    public class PaymentController : BaseController
	{
		private readonly IPaymentService _paymentService;
		public PaymentController(IPaymentService paymentService)
		{
			_paymentService = paymentService;
		}

		[HttpPost("checkout-url")]
		public async Task<IActionResult> Payment([FromBody] CheckoutUrlRequest request)
		{
			var user = GetUserId();
			string url = await _paymentService.CreatePaymentUrl(user, request, HttpContext);
			return Ok(Result<string>.Success(url));
		}

		[HttpGet("callback/{id}")]
		public async Task<IActionResult> Callback(Guid id, [FromQuery] PaymentCallbackModel param)
		{
			var response = await Mediator.Send(new OrderCheckoutCommand(id, param));
			if(response.Succeeded == false)
			{
				return Redirect("http://localhost:5173/checkout/failure");
			}
			return Redirect("http://localhost:5173/checkout/success");
		}
	}
}
