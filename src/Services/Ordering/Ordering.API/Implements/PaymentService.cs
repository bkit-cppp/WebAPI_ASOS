using BuildingBlock.Grpc.Services;
using Microsoft.Extensions.Options;
using Ordering.API.Extensions;
using Ordering.API.Interfaces;
using Ordering.API.Settings;

namespace Ordering.API.Implements;

public class PaymentService : IPaymentService
{
	private readonly DataContext _context;
	private readonly VnpaySettings _settings;
	private readonly BasketGrpcService _basketService;

	public PaymentService(DataContext context, IOptions<VnpaySettings> settings, BasketGrpcService basketService)
	{
		_settings = settings.Value;
		_context = context;
		_basketService = basketService;
	}

	public async Task<string> CreatePaymentUrl(Guid user, CheckoutUrlRequest request, HttpContext context)
	{
		try
		{
			decimal total = await _basketService.GetProductAsync(user);
			var order = await CreateTransaction(user, total, request);
			var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_settings.TimeZoneId);
			var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
			var tick = DateTime.Now.Ticks.ToString();
			var pay = new VnPayLibrary();
			var urlCallBack = _settings.ReturnUrl;

			pay.AddRequestData("vnp_Version", _settings.Version);
			pay.AddRequestData("vnp_Command", _settings.Command);
			pay.AddRequestData("vnp_TmnCode", _settings.TmnCode);
			pay.AddRequestData("vnp_Amount", ((int)total * 100000).ToString());
			pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
			pay.AddRequestData("vnp_CurrCode", _settings.CurrCode);
			pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
			pay.AddRequestData("vnp_Locale", _settings.Locale);
			pay.AddRequestData("vnp_OrderInfo", $"PAYMENT_CODE");
			pay.AddRequestData("vnp_OrderType", "course");
			pay.AddRequestData("vnp_ReturnUrl", $"{urlCallBack}/{order.Id}");
			pay.AddRequestData("vnp_TxnRef", tick);

			var paymentUrl = pay.CreateRequestUrl(_settings.BaseUrl, _settings.HashSecret);

			return paymentUrl;
		}
		catch (Exception ex)
		{
			return "";
		}
	}

	public PaymentResponseModel PaymentExecute(IQueryCollection collections)
	{
		var pay = new VnPayLibrary();
		var response = pay.GetFullResponseData(collections, _settings.HashSecret);

		return response;
	}

	private async Task<Order> CreateTransaction(Guid user, decimal total, CheckoutUrlRequest request)
	{
		var order = new Order()
		{
			UserId = user,
			Total = total,
			StatusId = OrderStatusConstant.Pending,
			ReceiverName = request.ReceiverName,
			Email = request.Email,
			Address = request.Address,
			Phone = request.Phone,
			ModifiedUser = user,
			CreatedUser = user
		};

		_context.Orders.Add(order);
		await _context.SaveChangesAsync();

		return order;
	}
}