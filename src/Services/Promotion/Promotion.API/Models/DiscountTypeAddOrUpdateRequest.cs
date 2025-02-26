namespace Promotion.API.Models;

public class DiscountTypeAddOrUpdateRequest : AddOrUpdateRequest
{
    public string? Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}
