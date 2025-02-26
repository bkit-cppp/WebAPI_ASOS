namespace Ordering.API.Models;

public class CheckoutUrlRequest
{
	public string ReceiverName { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public string Address { get; set; }
}
