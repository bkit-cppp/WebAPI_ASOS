using Catalog.Application.Features.CategoryFeature.Dto;

namespace Catalog.Application.Features.CategoryFeature.Queries;
public record Category_GetByGenderSlugQuery(string slug) : IQuery<Result<List<CategoryDto>>>;
public class Category_GetByGenderSlugQueryHandler : IQueryHandler<Category_GetByGenderSlugQuery, Result<List<CategoryDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Category_GetByGenderSlugQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<List<CategoryDto>>> Handle(Category_GetByGenderSlugQuery request, CancellationToken cancellationToken)
	{
		var id = await _unitOfWork.Genders.Queryable()
								  .Where(s => s.Slug == request.slug)
								  .Select(s => s.Id)
								  .FirstOrDefaultAsync();

		var categories = await _unitOfWork.CategoryGenders.Queryable()
									  .Where(s => s.GenderId == id)
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