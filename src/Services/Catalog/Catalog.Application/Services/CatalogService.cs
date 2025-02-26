using BuildingBlock.Grpc.Protos;
using Grpc.Core;

namespace Catalog.Application.Services;

public class CatalogService : CatalogGrpc.CatalogGrpcBase
{
	private readonly IUnitOfWork _unitOfWork;
	public CatalogService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public override async Task<GetProductReply> GetProduct(GetProductRequest request, ServerCallContext context)
	{
		var variation = await _unitOfWork.Variations.Queryable()
							  .Where(s => s.Id == Guid.Parse(request.Id) &&
										  s.ProductItem != null && s.ProductItem.Product != null &&
										  s.ProductItem.Color != null && s.Size != null && 
										  s.ProductItem.Product.Brand != null && s.ProductItem.Product.CategoryGender != null)
							  .Select(s => new GetProductReply()
							  {
								  Success = true,
								  ErrMessage = "",
								  VariationId = s.Id.ToString(),
								  ProductId = s.ProductItem!.ProductId.ToString(),
								  ProductItemId = s.ProductItemId.ToString(),
								  Slug = s.ProductItem.Product!.Slug,
								  Name = s.ProductItem.Product!.Name,
								  Description = "",
								  Category = s.ProductItem.Product!.CategoryGender!.Category.Name,
								  Brand = s.ProductItem.Product!.Brand!.Name,
								  Size = s.Size!.Name,
								  Color = s.ProductItem.Color!.Name,
								  AdditionalPrice = (double)s.ProductItem.AdditionalPrice,
								  OriginalPrice = (double)s.ProductItem.Product.OriginalPrice,
								  SalePrice = (double)s.ProductItem.Product.SalePrice,
								  Stock = (double)s.Stock,
								  IsSale = s.ProductItem.Product.IsSale,
								  QtyDisplay = s.QtyDisplay,
								  QtyInStock = s.QtyInStock,
								  Image = s.ProductItem.Images == null ? s.ProductItem.Product.Image : 
										  s.ProductItem.Images.Select(s => s.Url).FirstOrDefault() ?? s.ProductItem.Product.Image
							  })
							  .FirstOrDefaultAsync();

		if (variation != null)
		{
			return variation;
		}

		return new GetProductReply()
		{
			Success = false,
			ErrMessage = "Product not found."
		};
	}
}