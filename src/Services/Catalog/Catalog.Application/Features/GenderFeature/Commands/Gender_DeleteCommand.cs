namespace Catalog.Application.Features.GenderFeature.Commands;

public record Gender_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
public class Gender_DeleteCommandHandler : ICommandHandler<Gender_DeleteCommand, Result<bool>>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Gender_DeleteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<bool>> Handle(Gender_DeleteCommand request, CancellationToken cancellationToken)
	{
		if (request.RequestData.Ids == null)
			throw new ApplicationException("Ids not found");

		IEnumerable<Guid> ids = request.RequestData.Ids.Select(m => Guid.Parse(m)).ToList();
		var genders = await _unitOfWork.Genders.FindByIds(ids, true);

		_unitOfWork.Genders.SoftDeleteRange(genders, request.RequestData.ModifiedUser);

		int rows = await _unitOfWork.CompleteAsync();
		if (rows > 0)
		{
			await _unitOfWork.RemoveCacheAsync(CatalogCacheKey.Gender);
		}
		return Result<bool>.Success(true);
	}
}
