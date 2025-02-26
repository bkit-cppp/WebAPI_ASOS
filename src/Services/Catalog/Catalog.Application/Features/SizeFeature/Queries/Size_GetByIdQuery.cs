using Catalog.Application.Features.SizeFeature.Dto;

namespace Catalog.Application.Features.SizeFeature.Queries;

public record Size_GetByIdQuery(Guid Id) : IQuery<Result<SizeDto>>;
public class Size_GetByIdQueryHandler : IQueryHandler<Size_GetByIdQuery, Result<SizeDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Size_GetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<SizeDto>> Handle(Size_GetByIdQuery request, CancellationToken cancellationToken)
	{

		var Size = await _unitOfWork.Sizes.Queryable()
									  .Where(s => s.Id == request.Id)
									  .ProjectTo<SizeDto>(_mapper.ConfigurationProvider)
									  .FirstOrDefaultAsync();

		return Result<SizeDto>.Success(Size);
	}
}