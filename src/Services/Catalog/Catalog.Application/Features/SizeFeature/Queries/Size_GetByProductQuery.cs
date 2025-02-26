using Catalog.Application.Features.SizeFeature.Dto;

namespace Catalog.Application.Features.SizeFeature.Queries;

public record Size_GetByProductQuery(BaseRequest RequestData,Guid ProductId) : IQuery<Result<IEnumerable<SizeDto>>>;
public class Size_GetByProductQueryHandler : IQueryHandler<Size_GetByProductQuery, Result<IEnumerable<SizeDto>>>
{
	private readonly IUnitOfWork _unitOfWork;

	public Size_GetByProductQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<IEnumerable<SizeDto>>> Handle(Size_GetByProductQuery request, CancellationToken cancellationToken)
	{
		var categoryId = await _unitOfWork.Products.Queryable()
							  .Where(s => s.Id == request.ProductId && s.CategoryGender != null)
							  .Select(s => s.CategoryGender!.CategoryId)
							  .FirstOrDefaultAsync();

		if (categoryId == Guid.Empty)
		{
			throw new ApplicationException("Product or category not found");
		}

		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		IEnumerable<SizeDto> Sizes = await _unitOfWork.SizeCategories.Queryable()
										   .OrderedListQuery(orderCol, orderDir)
										   .Where(s => s.CategoryId == categoryId && s.SizeId != null)
										   .Select(s => new SizeDto()
										   {
											   Id = s.Size!.Id,
											   Name = s.Size.Name,
											   Description = s.Size.Description
										   })
										   .ToListAsync();

		return Result<IEnumerable<SizeDto>>.Success(Sizes);
	}
}