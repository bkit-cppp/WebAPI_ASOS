using Ordering.API.Features.RefundFeature.Dtos;

namespace Ordering.API.Features.RefundFeature.Queries;


public record Refund_GetFilterQueries(RefundFilterRequest RequestData) : IQuery<Result<IEnumerable<RefundDto>>>;
public class Refund_GetFilterQueriesHandler : IQueryHandler<Refund_GetFilterQueries, Result<IEnumerable<RefundDto>>>
{
	private readonly IMapper _mapper;
	private readonly DataContext _dataContext;
	public Refund_GetFilterQueriesHandler(IMapper mapper, DataContext dataContext)
	{
		_mapper = mapper;
		_dataContext = dataContext;
	}
	public async Task<Result<IEnumerable<RefundDto>>> Handle(Refund_GetFilterQueries request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _dataContext.Refunds.OrderedListQuery(orderCol, orderDir)
							.ProjectTo<RefundDto>(_mapper.ConfigurationProvider)
							.AsNoTracking();

		if (!StringHelper.GuidIsNull(request.RequestData.TransactionId))
		{
			query = query.Where(s => s.TransactionId == request.RequestData.TransactionId);
		}
		if (request.RequestData.Skip != null)
		{
			query = query.Skip(request.RequestData.Skip.Value);
		}
		if (request.RequestData.TotalRecord != null)
		{
			query = query.Take(request.RequestData.TotalRecord.Value);
		}

		return Result<IEnumerable<RefundDto>>.Success(await query.ToListAsync());
	}
}
