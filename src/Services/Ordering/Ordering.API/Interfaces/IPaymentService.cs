namespace Ordering.API.Interfaces;

public interface IPaymentService
{
	Task<string> CreatePaymentUrl(Guid user, CheckoutUrlRequest request, HttpContext context);
	PaymentResponseModel PaymentExecute(IQueryCollection collections);
}
