namespace Basket.API.Features.CartFeature.Dto;

public class DiscountDto
{
	public Guid Id { get; set; }
	public string Code { get; set; } = string.Empty;
	public decimal Value { get; set; } = 0;
	public string DiscountTypeId { get; set; }
}
