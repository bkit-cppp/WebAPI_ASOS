using Catalog.Application.Features.SizeFeature.Dto;

namespace Catalog.Application.Features.SizeFeature.Queries;

public record Size_GetByProductItemQuery(BaseRequest RequestData,Guid ProductId) : IQuery<Result<IEnumerable<SizeDto>>>;
public class Size_GetByProductItemQueryHandler : IQueryHandler<Size_GetByProductItemQuery, Result<IEnumerable<SizeDto>>>
{
	private readonly IUnitOfWork _unitOfWork;

	public Size_GetByProductItemQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<IEnumerable<SizeDto>>> Handle(Size_GetByProductItemQuery request, CancellationToken cancellationToken)
	{
		var categoryId = await _unitOfWork.ProductItems.Queryable()
							  .Include(s => s.Product)
							  .Where(s => s.Id == request.ProductId && s.Product != null && s.Product.CategoryGender != null)
							  .Select(s => s.Product!.CategoryGender!.CategoryId)
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