using BuildingBlock.Core.Abstractions;
using Identity.API.Features.PointHistoryFeature.Dto;
namespace Identity.API.Features.PointHistoryFeature.Queries;
public record PointHistory_GetByIdQuery(Guid id) : IQuery<Result<IEnumerable<PointHistoryDto>>>;
public class PointHistory_GetByIdQueryHandler : IQueryHandler<PointHistory_GetByIdQuery, Result<IEnumerable<PointHistoryDto>>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public PointHistory_GetByIdQueryHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Result<IEnumerable<PointHistoryDto>>> Handle(PointHistory_GetByIdQuery request, CancellationToken cancellationToken)
    {

        IEnumerable<PointHistoryDto> users = await _context.PointHistories.OrderByDescending(s => s.CreatedDate)
                                                   .Where(s => s.UserId == request.id)
                                                   .ProjectTo<PointHistoryDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync();

        return Result<IEnumerable<PointHistoryDto>>.Success(users);
    }

}
