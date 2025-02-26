using Identity.API.Features.PointHistoryFeature.Dto;

namespace Identity.API.Features.PointHistoryFeature.Queries;

public record PointHistory_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<PointHistoryDto>>>;
public class PointHistory_GetFilterQueryHandler : IQueryHandler<PointHistory_GetFilterQuery, Result<IEnumerable<PointHistoryDto>>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public PointHistory_GetFilterQueryHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<PointHistoryDto>>> Handle(PointHistory_GetFilterQuery request, CancellationToken cancellationToken)
    {
		var orderCol = request.RequestData.OrderCol ?? nameof(PointHistory.CreatedDate);
		var orderDir = request.RequestData.OrderDir ?? "desc";

		var query = _context.PointHistories.OrderedListQuery(orderCol, orderDir)
			                .Where(s => s.UserId == request.RequestData.UserId)
							.ProjectTo<PointHistoryDto>(_mapper.ConfigurationProvider)
                            .AsNoTracking();


        if (request.RequestData.Skip != null)
        {
            query = query.Skip(request.RequestData.Skip.Value);
        }

        if (request.RequestData.TotalRecord != null)
        {
            query = query.Take(request.RequestData.TotalRecord.Value);
        }

        return Result<IEnumerable<PointHistoryDto>>.Success(await query.ToListAsync());
    }
}