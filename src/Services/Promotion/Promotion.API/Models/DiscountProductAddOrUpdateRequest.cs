namespace Promotion.API.Models;

public class DiscountProductAddOrUpdateRequest : AddOrUpdateRequest
{
    public Guid DiscountId { get; set; }
    public List<Guid> ProductIds { get; set; }
}
