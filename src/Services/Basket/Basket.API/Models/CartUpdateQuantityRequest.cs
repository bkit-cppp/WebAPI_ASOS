using Basket.API.Enums;

namespace Basket.API.Models;

public class CartUpdateQuantityRequest
{
	public Guid UserId { get; set; }
	public Guid VariationId { get; set; }
	public CartUpdate Update { get; set; } 
}
