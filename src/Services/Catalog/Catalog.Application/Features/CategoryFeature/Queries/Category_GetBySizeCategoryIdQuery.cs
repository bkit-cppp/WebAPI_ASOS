using Catalog.Application.Features.CategoryFeature.Dto;
namespace Catalog.Application.Features.SizeFeature.Queries;

public record Category_GetBySizeQuery(Guid sizeId) : IQuery<Result<List<CategoryViewDto>>>;
public class Category_GetBySizeQueryHandler : IQueryHandler<Category_GetBySizeQuery, Result<List<CategoryViewDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Category_GetBySizeQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<CategoryViewDto>>> Handle(Category_GetBySizeQuery request, CancellationToken cancellationToken)
    {
		var ids = _unitOfWork.SizeCategories.Queryable()
							 .Where(sc => sc.SizeId == request.sizeId)
							 .Select(sc => sc.CategoryId)
							 .Distinct().ToHashSet();

		var categories = await _unitOfWork.Categories.Queryable()
						.Select(s => new CategoryViewDto
						{
							Id = s.Id,
							Name = s.Name,
							Slug = s.Slug,
							Selected = ids.Contains(s.Id)
						})
						.OrderByDescending(s => s.Selected)
						.ToListAsync();

		return Result<List<CategoryViewDto>>.Success(categories);
    }
}