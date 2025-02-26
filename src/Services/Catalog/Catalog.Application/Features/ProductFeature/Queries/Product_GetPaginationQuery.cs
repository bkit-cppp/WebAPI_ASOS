using BuildingBlock.Core.Paging;
using BuildingBlock.Utilities;
using Catalog.Application.Features.ProductFeature.Dto;
using Catalog.Application.Models.ProductModel;

namespace Catalog.Application.Features.ProductFeature.Queries;

public record Product_GetPaginationQuery(ProductPaginationRequest RequestData) : IQuery<Result<PaginatedList<ProductDto>>>;
public class Product_GetPaginationHandler : IQueryHandler<Product_GetPaginationQuery, Result<PaginatedList<ProductDto>>>
{
	private readonly IMapper _mapper;
	private readonly IUnitOfWork _unitOfWork;
	public Product_GetPaginationHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_mapper = mapper;
		_unitOfWork = unitOfWork;
	}
	public async Task<Result<PaginatedList<ProductDto>>> Handle(Product_GetPaginationQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _unitOfWork.Products.Queryable()
								 .OrderedListQuery(orderCol, orderDir)
								 .AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			query = query.Where(s => s.Name.Contains(request.RequestData.TextSearch));
		}

		if (!StringHelper.GuidIsNull(request.RequestData.CategoryId))
		{
			query = query.Where(s => s.CategoryGender != null && s.CategoryGender.CategoryId == request.RequestData.CategoryId);
		}

		if (!StringHelper.GuidIsNull(request.RequestData.GenderId))
		{
			query = query.Where(s => s.CategoryGender != null && s.CategoryGender.GenderId == request.RequestData.GenderId);
		}

		if (!StringHelper.GuidIsNull(request.RequestData.BrandId))
		{
			query = query.Where(s => s.BrandId == request.RequestData.BrandId);
		}

		var paging = await query.ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
								.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
		
		return Result<PaginatedList<ProductDto>>.Success(paging);
	}
}
