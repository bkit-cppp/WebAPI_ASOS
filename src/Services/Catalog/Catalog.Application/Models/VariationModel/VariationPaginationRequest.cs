namespace Catalog.Application.Models.VariationModel;

public class VariationPaginationRequest : PaginationRequest
{
	public Guid? ProductItemId { get; set; }
}
