namespace Promotion.API.Models;

public class DiscountAddOrUpdateRequest : AddOrUpdateRequest
{
    public Guid? Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Code { get; set; }
    public decimal Value { get; set; }
    public decimal Minimum { get; set; }
	public string Condition { get; set; } = string.Empty;
	public int Quantity { get; set; }
	public bool Available { get; set; } = false;
	public string DiscountTypeId { get; set; }
}
