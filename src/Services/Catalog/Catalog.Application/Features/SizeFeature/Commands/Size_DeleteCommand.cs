namespace Catalog.Application.Features.SizeFeature.Commands;

public record Size_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
public class Size_DeleteCommandHandler : ICommandHandler<Size_DeleteCommand, Result<bool>>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Size_DeleteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<bool>> Handle(Size_DeleteCommand request, CancellationToken cancellationToken)
	{
		if (request.RequestData.Ids == null)
			throw new ApplicationException("Ids not found");

		IEnumerable<Guid> ids = request.RequestData.Ids.Select(m => Guid.Parse(m)).ToList();
		var Sizes = await _unitOfWork.Sizes.FindByIds(ids, true);

		_unitOfWork.Sizes.SoftDeleteRange(Sizes, request.RequestData.ModifiedUser);

		int rows = await _unitOfWork.CompleteAsync();
		if (rows > 0)
		{
			await _unitOfWork.RemoveCacheAsync(CatalogCacheKey.Size);
		}
		return Result<bool>.Success(true);
	}
}
