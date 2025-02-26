using Identity.API.Features.NotificationFeature.Dto;

namespace Identity.API.Features.NotificationFeature.Queries;

public record Notification_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<NotificationDto>>>;
public class Notification_GetAllQueryHandler : IQueryHandler<Notification_GetAllQuery, Result<IEnumerable<NotificationDto>>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Notification_GetAllQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<NotificationDto>>> Handle(Notification_GetAllQuery request, CancellationToken cancellationToken)
	{
		IEnumerable<NotificationDto> Notifications = await _context.Notifications.OrderByDescending(s => s.CreatedDate)
												   .ProjectTo<NotificationDto>(_mapper.ConfigurationProvider)
												   .ToListAsync();

		return Result<IEnumerable<NotificationDto>>.Success(Notifications);
	}
}