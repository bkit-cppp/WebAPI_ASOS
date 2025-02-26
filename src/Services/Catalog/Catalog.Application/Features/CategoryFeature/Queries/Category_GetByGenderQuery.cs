using Catalog.Application.Features.CategoryFeature.Dto;

namespace Catalog.Application.Features.CategoryFeature.Queries;
public record Category_GetByGenderQuery(Guid genderId) : IQuery<Result<List<CategoryDto>>>;
public class Category_GetByGenderQueryHandler : IQueryHandler<Category_GetByGenderQuery, Result<List<CategoryDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Category_GetByGenderQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<List<CategoryDto>>> Handle(Category_GetByGenderQuery request, CancellationToken cancellationToken)
	{
		var categories = await _unitOfWork.CategoryGenders.Queryable()
									  .Where(s => s.GenderId == request.genderId)
									  .Select(s => new CategoryDto()
									  {
										  Id = s.Category.Id,
										  Name = s.Category.Name,
										  Slug = s.Category.Slug,
										  Description = s.Category.Description,
										  ImageFile = s.Category.ImageFile,
										  ParentId = s.Category.ParentId,
										  CreatedDate = s.Category.CreatedDate,
										  ModifiedDate = s.Category.ModifiedDate,
									  })
									  .ToListAsync();

		return Result<List<CategoryDto>>.Success(categories);
	}
}