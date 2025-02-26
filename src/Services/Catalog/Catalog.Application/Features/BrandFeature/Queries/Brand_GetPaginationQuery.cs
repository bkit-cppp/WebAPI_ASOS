using BuildingBlock.Core.Paging;
using Catalog.Application.Features.BrandFeature.Dto;

namespace Catalog.Application.Features.BrandFeature.Queries;

public record Brand_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<BrandDto>>>;
public class Brand_GetPaginationQueryHandler : IQueryHandler<Brand_GetPaginationQuery, Result<PaginatedList<BrandDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Brand_GetPaginationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<PaginatedList<BrandDto>>> Handle(Brand_GetPaginationQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _unitOfWork.Brands.Queryable()
							   .OrderedListQuery(orderCol, orderDir)
							   .ProjectTo<BrandDto>(_mapper.ConfigurationProvider)
							   .AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			query = query.Where(s => s.Name.Contains(request.RequestData.TextSearch));
		}

		var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
		return Result<PaginatedList<BrandDto>>.Success(paging);
	}
}