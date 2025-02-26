using Ordering.API.Features.OrderFeature.Dto;

namespace Ordering.API.Features.OrderFeature.Queries;

public record OrderGetPaginationQuery(PaginationRequest PaginationRequest) : IQuery<Result<PaginatedList<OrderDto>>>;
public class OrderGetPaginationHandler(DataContext dataContext, IMapper mapper) : IQueryHandler<OrderGetPaginationQuery, Result<PaginatedList<OrderDto>>>
{

	public async Task<Result<PaginatedList<OrderDto>>> Handle(OrderGetPaginationQuery request, CancellationToken cancellationToken)
	{
		var orders = dataContext.Orders.OrderByDescending(s => s.CreatedDate)
								.ProjectTo<OrderDto>(mapper.ConfigurationProvider)
								.AsNoTracking();

		if (!string.IsNullOrEmpty(request.PaginationRequest.TextSearch))
		{
			string text = request.PaginationRequest.TextSearch;
			orders = orders.Where(s => s.ReceiverName.Contains(text) ||
									   s.Email.Contains(text) ||
									   s.Phone.Contains(text) ||
									   s.Address.Contains(text));
		}

		if (!string.IsNullOrEmpty(request.PaginationRequest.Status))
		{
			orders = orders.Where(s => s.StatusId == request.PaginationRequest.Status);
		}

		if (!StringHelper.GuidIsNull(request.PaginationRequest.UserId))
		{
			orders = orders.Where(s => s.UserId == request.PaginationRequest.UserId);
		}

		var paging = await orders.PaginatedListAsync(request.PaginationRequest.PageIndex, request.PaginationRequest.PageSize);
		return Result<PaginatedList<OrderDto>>.Success(paging);
	}
}
