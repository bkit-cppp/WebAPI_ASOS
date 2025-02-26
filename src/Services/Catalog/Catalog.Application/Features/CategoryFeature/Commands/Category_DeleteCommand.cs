namespace Catalog.Application.Features.CategoryFeature.Commands;

public record Category_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
public class Category_DeleteCommandHandler : ICommandHandler<Category_DeleteCommand, Result<bool>>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Category_DeleteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<bool>> Handle(Category_DeleteCommand request, CancellationToken cancellationToken)
	{
		if (request.RequestData.Ids == null)
			throw new ApplicationException("Ids not found");

		IEnumerable<Guid> ids = request.RequestData.Ids.Select(m => Guid.Parse(m)).ToList();
		var Categorys = await _unitOfWork.Categories.FindByIds(ids, true);

		_unitOfWork.Categories.SoftDeleteRange(Categorys, request.RequestData.ModifiedUser);

		int rows = await _unitOfWork.CompleteAsync();
		if (rows > 0)
		{
			await _unitOfWork.RemoveCacheAsync(CatalogCacheKey.Category);
		}

		return Result<bool>.Success(true);
	}
}
