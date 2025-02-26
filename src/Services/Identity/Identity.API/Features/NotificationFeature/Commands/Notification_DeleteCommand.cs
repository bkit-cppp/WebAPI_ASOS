using BuildingBlock.Utilities;

namespace Identity.API.Features.NotificationFeature.Commands;

public record Notification_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
public class Notification_DeleteCommandHandler : ICommandHandler<Notification_DeleteCommand, Result<bool>>
{

	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Notification_DeleteCommandHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<bool>> Handle(Notification_DeleteCommand request, CancellationToken cancellationToken)
	{
		if (request.RequestData.Ids == null)
			throw new ApplicationException("Ids not found");

		if (StringHelper.GuidIsNull(request.RequestData.ModifiedUser))
			throw new ApplicationException("User not found");

		List<Guid> ids = request.RequestData.Ids.Select(m => Guid.Parse(m)).ToList();
		var query = await _context.Notifications
								  .Where(m => ids.Contains(m.Id) &&
											  m.UserId == request.RequestData.ModifiedUser)
								  .ToListAsync();

		if (query == null || query.Count == 0) throw new ApplicationException($"Không tìm thấy trong dữ liệu có Id: {string.Join(";", request.RequestData.Ids)}");

		foreach (var item in query)
		{
			item.DeleteFlag = true;
			item.ModifiedDate = DateTime.Now;
			item.ModifiedUser = request.RequestData.ModifiedUser;
		}

		_context.Notifications.UpdateRange(query);

		await _context.SaveChangesAsync(cancellationToken);

		return Result<bool>.Success(true);
	}
}
