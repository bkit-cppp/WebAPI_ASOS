using Catalog.Application.Features.BrandFeature.Dto;

namespace Catalog.Application.Features.BrandFeature.Queries;

public record Brand_GetByIdQuery(Guid Id) : IQuery<Result<BrandDto>>;
public class Brand_GetByIdQueryHandler : IQueryHandler<Brand_GetByIdQuery, Result<BrandDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Brand_GetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<BrandDto>> Handle(Brand_GetByIdQuery request, CancellationToken cancellationToken)
	{

		var Brand = await _unitOfWork.Brands.Queryable()
									  .Where(s => s.Id == request.Id)
									  .ProjectTo<BrandDto>(_mapper.ConfigurationProvider)
									  .FirstOrDefaultAsync();

		return Result<BrandDto>.Success(Brand);
	}
}