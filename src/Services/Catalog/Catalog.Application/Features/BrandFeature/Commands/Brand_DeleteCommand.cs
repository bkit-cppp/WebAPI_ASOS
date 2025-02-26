namespace Catalog.Application.Features.BrandFeature.Commands;

public record Brand_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
public class Brand_DeleteCommandHandler : ICommandHandler<Brand_DeleteCommand, Result<bool>>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Brand_DeleteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<bool>> Handle(Brand_DeleteCommand request, CancellationToken cancellationToken)
	{
		if (request.RequestData.Ids == null)
			throw new ApplicationException("Ids not found");

		IEnumerable<Guid> ids = request.RequestData.Ids.Select(m => Guid.Parse(m)).ToList();
		var Brands = await _unitOfWork.Brands.FindByIds(ids, true);

		_unitOfWork.Brands.SoftDeleteRange(Brands, request.RequestData.ModifiedUser);

		int rows= await _unitOfWork.CompleteAsync();
		if (rows > 0)
		{
			await _unitOfWork.RemoveCacheAsync(CatalogCacheKey.Brand);
		}

		return Result<bool>.Success(true);
	}
}
