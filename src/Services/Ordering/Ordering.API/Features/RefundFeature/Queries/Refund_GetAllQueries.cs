using Ordering.API.Features.RefundFeature.Dtos;

namespace Ordering.API.Features.RefundFeature.Queries;

public record Refund_GetAllQueries(BaseRequest RequestData) : IQuery<Result<IEnumerable<RefundDto>>>;
public class Refund_GetAllQueriesHandler : IQueryHandler<Refund_GetAllQueries, Result<IEnumerable<RefundDto>>>
{
	private readonly IMapper _mapper;
	private readonly DataContext _dataContext;
	public Refund_GetAllQueriesHandler(IMapper mapper, DataContext dataContext)
	{
		_mapper = mapper;
		_dataContext = dataContext;
	}
	public async Task<Result<IEnumerable<RefundDto>>> Handle(Refund_GetAllQueries request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		IEnumerable<RefundDto> reFund = await _dataContext.Refunds.OrderedListQuery(orderCol, orderDir)
											   .ProjectTo<RefundDto>(_mapper.ConfigurationProvider)
											   .ToListAsync();

		return Result<IEnumerable<RefundDto>>.Success(reFund);
	}
}
