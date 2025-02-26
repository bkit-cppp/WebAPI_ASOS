using Catalog.Application.Features.BrandFeature.Dto;

namespace Catalog.Application.Features.BrandFeature.Queries;

public record Brand_GetBySlugQuery(string Slug) : IQuery<Result<BrandDto>>;
public class Brand_GetBySlugQueryHandler : IQueryHandler<Brand_GetBySlugQuery, Result<BrandDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Brand_GetBySlugQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<BrandDto>> Handle(Brand_GetBySlugQuery request, CancellationToken cancellationToken)
	{

		var Brand = await _unitOfWork.Brands.Queryable()
									  .Where(s => s.Slug == request.Slug)
									  .ProjectTo<BrandDto>(_mapper.ConfigurationProvider)
									  .FirstOrDefaultAsync();

		return Result<BrandDto>.Success(Brand);
	}
}