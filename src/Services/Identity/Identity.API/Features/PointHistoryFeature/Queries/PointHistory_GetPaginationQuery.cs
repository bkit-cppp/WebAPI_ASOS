using BuildingBlock.Core.Paging;
using Identity.API.Features.PointHistoryFeature.Dto;

namespace Identity.API.Features.PointHistoryFeature.Queries;

public record PointHistory_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<PointHistoryDto>>>;
public class User_GetPaginationQueryHandler : IQueryHandler<PointHistory_GetPaginationQuery, Result<PaginatedList<PointHistoryDto>>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public User_GetPaginationQueryHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<PointHistoryDto>>> Handle(PointHistory_GetPaginationQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol ?? nameof(PointHistory.CreatedDate);
        var orderDir = request.RequestData.OrderDir ?? "desc";

        var query = _context.PointHistories.OrderedListQuery(orderCol, orderDir)
                            .Where(s => s.UserId == request.RequestData.UserId)
                            .ProjectTo<PointHistoryDto>(_mapper.ConfigurationProvider)
                            .AsNoTracking();

        var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
        return Result<PaginatedList<PointHistoryDto>>.Success(paging);
    }
}