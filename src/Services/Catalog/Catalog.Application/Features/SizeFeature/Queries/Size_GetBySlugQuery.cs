using Catalog.Application.Features.SizeFeature.Dto;
namespace Catalog.Application.Features.SizeFeature.Queries;

public record Size_GetBySlugQuery(string Slug) : IQuery<Result<SizeDto>>;
public class Size_GetBySlugQueryHandler : IQueryHandler<Size_GetBySlugQuery, Result<SizeDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Size_GetBySlugQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<SizeDto>> Handle(Size_GetBySlugQuery request, CancellationToken cancellationToken)
	{

		var Size = await _unitOfWork.Sizes.Queryable()
									  .Where(s => s.Slug == request.Slug)
									  .ProjectTo<SizeDto>(_mapper.ConfigurationProvider)
									  .FirstOrDefaultAsync();

		return Result<SizeDto>.Success(Size);
	}
}