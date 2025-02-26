using BuildingBlock.Core.Paging;
using BuildingBlock.Utilities;
using Catalog.Application.Features.VariationFeature.Dto;
using Catalog.Application.Models.VariationModel;

namespace Catalog.Application.Features.VariationFeature.Queries;

public record Variation_GetPaginationQuery(VariationPaginationRequest RequestData) : IQuery<Result<PaginatedList<VariationDto>>>;
public class Product_GetPaginationHandler : IQueryHandler<Variation_GetPaginationQuery, Result<PaginatedList<VariationDto>>>
{
	private readonly IMapper _mapper;
	private readonly IUnitOfWork _unitOfWork;
	public Product_GetPaginationHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_mapper = mapper;
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<PaginatedList<VariationDto>>> Handle(Variation_GetPaginationQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var variations = _unitOfWork.Variations.Queryable()
									.OrderedListQuery(orderCol, orderDir)
									.ProjectTo<VariationDto>(_mapper.ConfigurationProvider)
									.AsNoTracking();

		if (!StringHelper.GuidIsNull(request.RequestData.ProductItemId))
		{
			variations = variations.Where(s => s.ProductItemId == request.RequestData.ProductItemId!.Value);
		}

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch) && int.TryParse(request.RequestData.TextSearch, out int searchQtyInStock))
		{
			variations = variations.Where(s => s.QtyInStock == searchQtyInStock);
		}

		var paging = await variations.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
		return Result<PaginatedList<VariationDto>>.Success(paging);
	}
}
