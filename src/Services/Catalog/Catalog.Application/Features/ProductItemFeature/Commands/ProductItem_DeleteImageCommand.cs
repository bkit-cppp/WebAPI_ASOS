namespace Catalog.Application.Features.ProductItemFeature.Commands;

public record ProductItem_DeleteImageCommand(DeleteRequest DeleteRequest) : ICommand<Result<bool>>;
public class ProductItem_DeleteImageCommandHandler : ICommandHandler<ProductItem_DeleteImageCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public ProductItem_DeleteImageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}
	public async Task<Result<bool>> Handle(ProductItem_DeleteImageCommand request, CancellationToken cancellationToken)
	{
		if (request.DeleteRequest.Ids == null)
			throw new ApplicationException("Ids not found");

		IEnumerable<Guid> ids = request.DeleteRequest.Ids.Select(m => Guid.Parse(m)).ToList();
		var images = await _unitOfWork.Images.FindByIds(ids, true);

		_unitOfWork.Images.SoftDeleteRange(images, request.DeleteRequest.ModifiedUser);

		await _unitOfWork.CompleteAsync();
		return Result<bool>.Success(true);
	}
}
