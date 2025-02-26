using Catalog.Application.Models.ProductItemModel;
namespace Catalog.Application.Features.ProductItemFeature.Commands;

public record ProductItem_AddImageCommand(AddProductImage RequestData) : ICommand<Result<bool>>;
public class Product_AddImageCommandHandler : ICommandHandler<ProductItem_AddImageCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;
	public Product_AddImageCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<Result<bool>> Handle(ProductItem_AddImageCommand request, CancellationToken cancellationToken)
	{
		var product = await _unitOfWork.ProductItems.FindAsync(request.RequestData.ProductItemId, true);
		List<Image> images = new List<Image>();
		foreach(var url in request.RequestData.Urls)
		{
			Image image = new Image()
			{
				ProductItemId = product!.Id,
				Url = url
			};
			images.Add(image);
		}
		_unitOfWork.Images.AddRange(images);
		await _unitOfWork.CompleteAsync();
		return Result<bool>.Success(true);

	}
}
