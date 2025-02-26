namespace Identity.API.Features.StatusFeature.Commands;

public record Status_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
public class Status_DeleteCommandHandler : ICommandHandler<Status_DeleteCommand, Result<bool>>
{

	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Status_DeleteCommandHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<bool>> Handle(Status_DeleteCommand request, CancellationToken cancellationToken)
	{
		if (request.RequestData.Ids == null)
			throw new ApplicationException("Ids not found");

		var query = await _context.Statuses.Where(m => request.RequestData.Ids.Contains(m.Id)).ToListAsync();
		if (query == null || query.Count == 0) throw new ApplicationException($"Không tìm thấy trong dữ liệu có Id: {string.Join(";", request.RequestData.Ids)}");

		foreach (var item in query)
		{
			item.DeleteFlag = true;
			item.ModifiedDate = DateTime.Now;
			item.ModifiedUser = request.RequestData.ModifiedUser;
		}

		_context.Statuses.UpdateRange(query);

		await _context.SaveChangesAsync(cancellationToken);

		return Result<bool>.Success(true);
	}
}
