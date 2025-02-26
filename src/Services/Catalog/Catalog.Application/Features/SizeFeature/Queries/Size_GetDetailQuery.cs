using Catalog.Application.Features.CategoryFeature.Dto;
using Catalog.Application.Features.SizeFeature.Dto;
namespace Catalog.Application.Features.SizeFeature.Queries;

public record Size_GetDetailQuery(Guid id) : IQuery<Result<SizeDetailDto>>;
public class Size_GetDetailQueryHandler : IQueryHandler<Size_GetDetailQuery, Result<SizeDetailDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public Size_GetDetailQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<SizeDetailDto>> Handle(Size_GetDetailQuery request, CancellationToken cancellationToken)
	{
		var size = await _unitOfWork.Sizes.Queryable()
									.Where(s => s.Id == request.id)
									.Select(s => new SizeDetailDto()
									{
										Id = s.Id,
										Name = s.Name,
										Slug = s.Slug,
										Description = s.Description
									})
									.FirstOrDefaultAsync();

		if(size == null)
		{
			throw new ApplicationException($"Data not found: {request.id}");
		}

		var ids = _unitOfWork.SizeCategories.Queryable()
					 .Where(sc => sc.SizeId == request.id)
					 .Select(sc => sc.CategoryId)
					 .Distinct().ToHashSet();

		size.Categories = await _unitOfWork.Categories.Queryable()
						.Select(s => new CategoryViewDto
						{
							Id = s.Id,
							Name = s.Name,
							Slug = s.Slug,
							Selected = ids.Contains(s.Id)
						})
						.OrderByDescending(s => s.Selected)
						.ToListAsync();

		return Result<SizeDetailDto>.Success(size);
	}
}