using BuildingBlock.Core.Paging;
using Catalog.Application.Features.SizeCategoryFeature.Dto;

namespace Catalog.Application.Features.SizeCategoryFeature.Queries;
public record SizeCategory_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<SizeCategoryDto>>>;
public class SizeCategory_GetPaginationQueryHandler : IQueryHandler<SizeCategory_GetPaginationQuery, Result<PaginatedList<SizeCategoryDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SizeCategory_GetPaginationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<SizeCategoryDto>>> Handle(SizeCategory_GetPaginationQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        var query = _unitOfWork.SizeCategories.Queryable()
                               .OrderedListQuery(orderCol, orderDir)
                               .ProjectTo<SizeCategoryDto>(_mapper.ConfigurationProvider)
                               .AsNoTracking();

        if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
        {
            query = query.Where(s => s.Id.ToString().Contains(request.RequestData.TextSearch));
        }

        var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
        return Result<PaginatedList<SizeCategoryDto>>.Success(paging);
    }
}