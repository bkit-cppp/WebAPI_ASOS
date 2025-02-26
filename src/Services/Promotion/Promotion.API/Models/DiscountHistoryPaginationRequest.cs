namespace Promotion.API.Models;

public class DiscountHistoryPaginationRequest : PaginationRequest
{
	public Guid? DiscountId { get; set; }
}
