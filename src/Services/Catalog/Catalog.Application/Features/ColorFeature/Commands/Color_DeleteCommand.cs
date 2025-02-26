namespace Catalog.Application.Features.ColorFeature.Commands;

public record Color_DeleteCommand(DeleteRequest DeleteRequest) : ICommand<Result<bool>>;
public class Color_DeleteCommandHandler : ICommandHandler<Color_DeleteCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public Color_DeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}
	public async Task<Result<bool>> Handle(Color_DeleteCommand request, CancellationToken cancellationToken)
	{
		if (request.DeleteRequest.Ids == null)
			throw new ApplicationException("Ids not Found");

		IEnumerable<Guid> ids = request.DeleteRequest.Ids.Select(m => Guid.Parse(m)).ToList();
		var colors = await _unitOfWork.Colors.FindByIds(ids, true);

		_unitOfWork.Colors.SoftDeleteRange(colors, request.DeleteRequest.ModifiedUser);
		int rows = await _unitOfWork.CompleteAsync();
		if (rows > 0)
		{
			await _unitOfWork.RemoveCacheAsync(CatalogCacheKey.Color);
		}

		return Result<bool>.Success(true);

	}
}
