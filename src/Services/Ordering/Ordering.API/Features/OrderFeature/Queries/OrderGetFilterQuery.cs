using Ordering.API.Data;
using Ordering.API.Features.OrderFeature.Dto;

namespace Ordering.API.Features.OrderFeature.Queries;

public record OrderGetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<OrderDto>>>;

public class OrderGetFilterQueryHandler : IQueryHandler<OrderGetFilterQuery, Result<IEnumerable<OrderDto>>>
{
	private readonly IMapper _mapper;
	private readonly DataContext _dataContext;
	public OrderGetFilterQueryHandler(DataContext dataContext, IMapper mapper)
	{
		_mapper = mapper;
		_dataContext = dataContext;
	}
	public async Task<Result<IEnumerable<OrderDto>>> Handle(OrderGetFilterQuery request, CancellationToken cancellationToken)
	{
		var query = _dataContext.Orders.OrderByDescending(s => s.CreatedDate)
							.ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
							.AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			string text = request.RequestData.TextSearch;
			query = query.Where(s => s.ReceiverName.Contains(text) ||
									   s.Email.Contains(text) ||
									   s.Phone.Contains(text) ||
									   s.Address.Contains(text));
		}

		if (!string.IsNullOrEmpty(request.RequestData.Status))
		{
			query = query.Where(s => s.StatusId == request.RequestData.Status);
		}

		if (request.RequestData.Skip != null)
		{
			query = query.Skip(request.RequestData.Skip.Value);
		}

		if (request.RequestData.TotalRecord != null)
		{
			query = query.Take(request.RequestData.TotalRecord.Value);
		}

		return Result<IEnumerable<OrderDto>>.Success(await query.ToListAsync());
	}
}
