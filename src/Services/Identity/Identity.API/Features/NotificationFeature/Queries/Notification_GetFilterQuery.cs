using BuildingBlock.Utilities;
using Identity.API.Features.NotificationFeature.Dto;

namespace Identity.API.Features.NotificationFeature.Queries;

public record Notification_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<NotificationDto>>>;
public class Notification_GetFilterQueryHandler : IQueryHandler<Notification_GetFilterQuery, Result<IEnumerable<NotificationDto>>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Notification_GetFilterQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<NotificationDto>>> Handle(Notification_GetFilterQuery request, CancellationToken cancellationToken)
	{
		if (StringHelper.GuidIsNull(request.RequestData.UserId))
		{
			return Result<IEnumerable<NotificationDto>>.Success(new List<NotificationDto>());
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

		if (request.RequestData.Skip != null)
		{
			query = query.Skip(request.RequestData.Skip.Value);
		}

		if (request.RequestData.TotalRecord != null)
		{
			query = query.Take(request.RequestData.TotalRecord.Value);
		}

		return Result<IEnumerable<NotificationDto>>.Success(await query.ToListAsync());
	}
}