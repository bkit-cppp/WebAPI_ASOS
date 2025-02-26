using Catalog.Application.Features.GenderFeature.Dto;
namespace Catalog.Application.Features.GenderFeature.Queries;

public record Gender_GetWithCategoryQuery() : IQuery<Result<IEnumerable<GenderMenuDto>>>;
public class Gender_GetWithCategoryQueryHandler : IQueryHandler<Gender_GetWithCategoryQuery, Result<IEnumerable<GenderMenuDto>>>
{
	private readonly IUnitOfWork _unitOfWork;

	public Gender_GetWithCategoryQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<IEnumerable<GenderMenuDto>>> Handle(Gender_GetWithCategoryQuery request, CancellationToken cancellationToken)
	{
		var categoryGenders = await _unitOfWork.CategoryGenders.Queryable()
			.Include(cg => cg.Category)
			.ToListAsync(cancellationToken);

		var genders = await _unitOfWork.Genders.Queryable().ToListAsync(cancellationToken);

		var result = genders.Select(gender => new GenderMenuDto
		{
			Id = gender.Id,
			Slug = gender.Slug,
			Name = gender.Name,
			Categories = BuildCategoryTree(categoryGenders, null, gender.Id)
		});

		return Result<IEnumerable<GenderMenuDto>>.Success(result);
	}

	private static List<CategoryInGender> BuildCategoryTree(IEnumerable<CategoryGender> categoryGenders, Guid? parentId, Guid genderId)
	{
		return categoryGenders
			.Where(cg => cg.Category.ParentId == parentId && cg.GenderId == genderId) // Lọc theo ParentId và GenderId
			.Select(cg => new CategoryInGender
			{
				Id = cg.Category.Id,
				Slug = cg.Category.Slug,
				Name = cg.Category.Name,
				ParentId = cg.Category.ParentId,
				Children = BuildCategoryTree(categoryGenders, cg.Category.Id, genderId) // Đệ quy cho sub-category
			})
			.ToList();
	}
}