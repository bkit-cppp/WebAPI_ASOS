namespace Promotion.API.Models;

public class DiscountHistoryAddOrUpdateRequest : AddOrUpdateRequest
{ 
    public Guid? Id { get; set; }
    public decimal DiscountApplied { get; set; }
    public Guid DiscountId { get; set; }
}
