using Catalog.Application.Features.CategoryFeature.Dto;

namespace Catalog.Application.Features.CategoryFeature.Queries;

public record Category_GetByIdQuery(Guid Id) : IQuery<Result<CategoryDto>>;
public class Category_GetByIdQueryHandler : IQueryHandler<Category_GetByIdQuery, Result<CategoryDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Category_GetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<CategoryDto>> Handle(Category_GetByIdQuery request, CancellationToken cancellationToken)
	{

		var Category = await _unitOfWork.Categories.Queryable()
									  .Where(s => s.Id == request.Id)
									  .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
									  .FirstOrDefaultAsync();

		return Result<CategoryDto>.Success(Category);
	}
}