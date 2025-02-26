namespace Basket.API.Models;

public class CartAddOrDeleteProductRequest
{
	public Guid UserId { get; set; }
	public Guid VariationId { get; set; }
}
