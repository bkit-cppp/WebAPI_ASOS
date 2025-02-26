using Identity.API.Features.NotificationFeature.Dto;

namespace Identity.API.Features.NotificationFeature.Queries;

public record Notification_GetByIdQuery(Guid Id) : IQuery<Result<NotificationDto>>;
public class Notification_GetByIdQueryHandler : IQueryHandler<Notification_GetByIdQuery, Result<NotificationDto>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Notification_GetByIdQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<NotificationDto>> Handle(Notification_GetByIdQuery request, CancellationToken cancellationToken)
	{
		var Notifications = await _context.Notifications.Where(s => s.Id == request.Id)
								  .ProjectTo<NotificationDto>(_mapper.ConfigurationProvider)
								  .FirstOrDefaultAsync();
		return Result<NotificationDto>.Success(Notifications);
	}
}