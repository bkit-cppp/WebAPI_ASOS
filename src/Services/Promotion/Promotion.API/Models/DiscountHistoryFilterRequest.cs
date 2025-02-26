namespace Promotion.API.Models;

public class DiscountHistoryFilterRequest : FilterRequest
{
	public Guid? DiscountId { get; set; }
}
