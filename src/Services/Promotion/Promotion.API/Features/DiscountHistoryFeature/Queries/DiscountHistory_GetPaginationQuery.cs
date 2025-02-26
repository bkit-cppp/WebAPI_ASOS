using BuildingBlock.Core.Paging;
using BuildingBlock.Utilities;
using Promotion.API.Features.DiscountHistoryFeature.Dto;
using Promotion.API.Models;
namespace Promotion.API.Features.DiscountHistoryFeature.Queries;

public record DiscountHistory_GetPaginationQuery(DiscountHistoryPaginationRequest RequestData) : IQuery<Result<PaginatedList<DiscountHistoryDto>>>;
public class DiscountHistory_GetPaginationQueryHandler : IQueryHandler<DiscountHistory_GetPaginationQuery, Result<PaginatedList<DiscountHistoryDto>>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public DiscountHistory_GetPaginationQueryHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<DiscountHistoryDto>>> Handle(DiscountHistory_GetPaginationQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        var query = _context.DiscountHistories.OrderedListQuery(orderCol, orderDir)
                            .ProjectTo<DiscountHistoryDto>(_mapper.ConfigurationProvider)
                            .AsNoTracking();

        if (!StringHelper.GuidIsNull(request.RequestData.DiscountId))
        {
            query = query.Where(s => s.DiscountId == request.RequestData.DiscountId);
        }

        var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
        return Result<PaginatedList<DiscountHistoryDto>>.Success(paging);
    }
}
