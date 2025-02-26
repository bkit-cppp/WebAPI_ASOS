using BuildingBlock.Core.Paging;
using BuildingBlock.Utilities;
using Identity.API.Features.NotificationFeature.Dto;

namespace Identity.API.Features.NotificationFeature.Queries;

public record Notification_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<NotificationDto>>>;
public class Notification_GetPaginationQueryHandler : IQueryHandler<Notification_GetPaginationQuery, Result<PaginatedList<NotificationDto>>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Notification_GetPaginationQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<PaginatedList<NotificationDto>>> Handle(Notification_GetPaginationQuery request, CancellationToken cancellationToken)
	{
		if (StringHelper.GuidIsNull(request.RequestData.UserId))
		{
			var tmp = PaginatedList<NotificationDto>.Empty(request.RequestData.PageIndex);
			return Result<PaginatedList<NotificationDto>>.Success(tmp);
		}

		var query = _context.Notifications.OrderByDescending(s => s.CreatedDate)
							.Where(s => s.UserId == request.RequestData.UserId)
							.ProjectTo<NotificationDto>(_mapper.ConfigurationProvider)
							.AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			query = query.Where(s => s.Title.Contains(request.RequestData.TextSearch) ||
									 s.Content.Contains(request.RequestData.TextSearch));
		}

		var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
		return Result<PaginatedList<NotificationDto>>.Success(paging);
	}
}