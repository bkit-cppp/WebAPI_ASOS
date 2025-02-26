using BuildingBlock.Core.Paging;
using Catalog.Application.Features.WishListFeature.Dto;

namespace Catalog.Application.Features.WishListFeature.Queries;
public record WishList_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<WishListDto>>>;
public class WishList_GetPaginationQueryHandler : IQueryHandler<WishList_GetPaginationQuery, Result<PaginatedList<WishListDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WishList_GetPaginationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<WishListDto>>> Handle(WishList_GetPaginationQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        var query = _unitOfWork.Wishlists.Queryable()
                               .OrderedListQuery(orderCol, orderDir)
                               .ProjectTo<WishListDto>(_mapper.ConfigurationProvider)
                               .AsNoTracking();

        if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
        {
            query = query.Where(s => s.Id.ToString().Contains(request.RequestData.TextSearch));
        }

        var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
        return Result<PaginatedList<WishListDto>>.Success(paging);
    }
}