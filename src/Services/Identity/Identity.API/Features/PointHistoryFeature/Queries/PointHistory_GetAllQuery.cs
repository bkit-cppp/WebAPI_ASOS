using BuildingBlock.Core.Abstractions;
using Identity.API.Features.PointHistoryFeature.Dto;
namespace Identity.API.Features.PointHistoryFeature.Queries;
public record PointHistory_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<PointHistoryDto>>>;
public class PointHistory_GetAllQueryHandler : IQueryHandler<PointHistory_GetAllQuery, Result<IEnumerable<PointHistoryDto>>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public PointHistory_GetAllQueryHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Result<IEnumerable<PointHistoryDto>>> Handle(PointHistory_GetAllQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        IEnumerable<PointHistoryDto> users = await _context.PointHistories.OrderedListQuery(orderCol, orderDir)
                                                   .ProjectTo<PointHistoryDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync();

        return Result<IEnumerable<PointHistoryDto>>.Success(users);
    }

}
