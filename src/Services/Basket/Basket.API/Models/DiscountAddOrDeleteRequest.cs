namespace Basket.API.Models;

public class DiscountAddOrDeleteRequest
{
	public Guid UserId { get; set; }
	public string Code { get; set; }
}
