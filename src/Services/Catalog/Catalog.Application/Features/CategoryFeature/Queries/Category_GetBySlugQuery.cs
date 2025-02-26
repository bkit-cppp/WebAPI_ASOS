using Catalog.Application.Features.CategoryFeature.Dto;

namespace Catalog.Application.Features.CategoryFeature.Queries;
public record Category_GetBySlugQuery(string Slug) : IQuery<Result<CategoryDto>>;
public class Category_GetBySlugQueryHandler : IQueryHandler<Category_GetBySlugQuery, Result<CategoryDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Category_GetBySlugQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<CategoryDto>> Handle(Category_GetBySlugQuery request, CancellationToken cancellationToken)
	{

		var Category = await _unitOfWork.Categories.Queryable()
									  .Where(s => s.Slug == request.Slug)
									  .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
									  .FirstOrDefaultAsync();

		return Result<CategoryDto>.Success(Category);
	}
}