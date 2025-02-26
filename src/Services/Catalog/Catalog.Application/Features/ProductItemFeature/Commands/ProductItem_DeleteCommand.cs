namespace Catalog.Application.Features.ProductItemFeature.Commands;

public record ProductItem_DeleteCommand(DeleteRequest DeleteRequest) : ICommand<Result<bool>>;
public class ProductItem_DeleteCommandHandler : ICommandHandler<ProductItem_DeleteCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public ProductItem_DeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}
	public async Task<Result<bool>> Handle(ProductItem_DeleteCommand request, CancellationToken cancellationToken)
	{
		if (request.DeleteRequest.Ids == null)
			throw new ApplicationException("Ids not found");

		IEnumerable<Guid> ids = request.DeleteRequest.Ids.Select(m => Guid.Parse(m)).ToList();
		var products = await _unitOfWork.ProductItems.FindByIds(ids, true);

		_unitOfWork.ProductItems.SoftDeleteRange(products, request.DeleteRequest.ModifiedUser);

		await _unitOfWork.CompleteAsync();
		return Result<bool>.Success(true);
	}
}
